﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using OrdersDb.Domain.Services.Orders.OrderItem;
using OrdersDb.Domain.Services.SystemServices;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Domain.Services.Orders.Order
{
    public class OrderService : ServiceBase<Order, OrderSearchParameters, OrderDto>, IOrderService
    {
        public OrderService(IAppDbContext db, IObjectContext objectContext)
            : base(db, objectContext)
        {
        }

        public override List<OrderDto> Search(OrderSearchParameters @params)
        {
            var query = Db.Set<Order>()
                .Include(x => x.Code)
                .Include(x => x.OrderItems)
                .AsQueryable();

            query = SearchByIds(query, @params);

            if (!string.IsNullOrEmpty(@params.Code))
                query = query.Where(x => x.Code.Value.ToLower().Contains(@params.Code.ToLower()));

            if (@params.MinBuyPrice != null)
                query = query.Where(x => x.OrderItems.Sum(oi => oi.Product.BuyPrice * oi.Amount) >= @params.MinBuyPrice);

            if (@params.MaxBuyPrice != null)
                query = query.Where(x => x.OrderItems.Sum(oi => oi.Product.BuyPrice * oi.Amount) <= @params.MaxBuyPrice);

            if (@params.MinSellPrice != null)
                query = query.Where(x => x.OrderItems.Sum(oi => oi.Product.SellPrice * oi.Amount) >= @params.MinSellPrice);

            if (@params.MaxSellPrice != null)
                query = query.Where(x => x.OrderItems.Sum(oi => oi.Product.SellPrice * oi.Amount) <= @params.MaxSellPrice);

            if (!string.IsNullOrEmpty(@params.ClientName))
                query = query.Where(x => x.Client.Name.ToLower().Contains(@params.ClientName.ToLower()));

            var result = query.OrderByTakeSkip(@params).Select(x => new OrderDto
                                                                       {
                                                                           Id = x.Id,
                                                                           Code = x.Code.Value,
                                                                           OrderItems = x.OrderItems.Select(oi => new OrderItemDto
                                                                                                                  {
                                                                                                                      Amount = oi.Amount,
                                                                                                                      ProductBuyPrice = oi.Product.BuyPrice,
                                                                                                                      ProductSellPrice = oi.Product.SellPrice
                                                                                                                  }).ToList(),
                                                                           ClientName = x.Client.Name,
                                                                           TotalItems = x.OrderItems.Count
                                                                       }).ToList();
            return result;
        }

        public override OrderDto GetById(int id)
        {
            var orderDto = new OrderDto();

            if (id != 0)
            {
                orderDto = Db.Set<Order>()
                    .Include(x => x.OrderItems)
                    .Include(x => x.Code)
                    .Where(x => x.Id == id)
                    .Select(x => new OrderDto
                                 {
                                     Id = x.Id,
                                     Code = x.Code.Value,
                                     CodeId = x.Code.Id,
                                     ClientName = x.Client.Name,
                                     ClientId = x.Client.Id,
                                     TotalItems = x.OrderItems.Count,
                                     OrderItems = x.OrderItems.Select(o => new OrderItemDto
                                                                           {
                                                                               Amount = o.Amount,
                                                                               Id = o.Id,
                                                                               ProductName = o.Product.Name,
                                                                               ProductId = o.Product.Id,
                                                                               ProductBuyPrice = o.Product.BuyPrice,
                                                                               ProductSellPrice = o.Product.SellPrice
                                                                           }).ToList()
                                 }).Single();
            }

            orderDto.Clients = Db.Clients
                .Select(x => new NameValue { Id = x.Id, Name = x.Name })
                .OrderBy(x => x.Name)
                .ToList();

            orderDto.Products = Db.Products
                .Select(x => new ProductPriceDto
                             {
                                 Id = x.Id,
                                 Name = x.Name,
                                 BuyPrice = x.BuyPrice,
                                 SellPrice = x.SellPrice
                             })
                .OrderBy(x => x.Name)
                .ToList();

            return orderDto;
        }

        public override void Update(Order entity)
        {
            //Валидация и аттач
            Validate(entity);

            entity.OrderItems.ForEach(x => Db.AttachIfDetached(x));

            var dbOrder = Db.Set<Order>()
                .Include(x => x.OrderItems)
                .Include(x => x.Code)
                .Include(x => x.Client)
                .Single(x => x.Id == entity.Id);

            //Добавляем новые элементы заказа
            var newOrderItems = entity.OrderItems.Where(x => x.Id == 0).ToList();
            Db.Set<OrderItem.OrderItem>().AddRange(newOrderItems);

            //Удаляем помеченные на удаление элементы заказа
            var modifiedOrderItemsIds = entity.OrderItems.Where(x => x.Id != 0).Select(x => x.Id).ToList();
            var orderItemsToDelete = dbOrder.OrderItems.Where(x => !modifiedOrderItemsIds.Contains(x.Id)).ToList();
            Db.Set<OrderItem.OrderItem>().RemoveRange(orderItemsToDelete);

            //Обновляем измененные элементы заказа
            var orderItemsToUpdate = dbOrder.OrderItems.Where(x => modifiedOrderItemsIds.Contains(x.Id)).ToList();
            orderItemsToUpdate.ForEach(x => Db.Entry(x).SetModifiedProperties(p => p.Amount, p => p.OrderId, p => p.ProductId));

            //Обновляем основную сущность
            dbOrder.OrderItems.Clear();
            dbOrder.OrderItems.AddRange(entity.OrderItems);
            dbOrder.ClientId = entity.ClientId;
            Db.SaveChanges();
        }

        public override void Add(Order entity)
        {
            //Валидация и аттач
            Validate(entity);

            entity.OrderItems.ForEach(x => Db.AttachIfDetached(x));

            var code = Db.Set<Code>().Include(x => x.Order).First(x => x.Order == null);
            entity.CodeId = code.Id;
            entity.Id = code.Id;
            entity.OrderItems.ForEach(x => x.OrderId = code.Id);

            //Аттач
            entity.OrderItems.ForEach(x => Db.AttachIfDetached(x));
            Db.AttachIfDetached(entity);

            //Изменение
            Db.Entry(entity).State = EntityState.Added;
            entity.OrderItems.ForEach(x => Db.Entry(x).State = EntityState.Added);

            Db.SaveChanges();
        }

        protected override void Validate(Order entity)
        {
            //Валидация и аттач
            var errors = entity.OrderItems.SelectMany(x => x.GetValidationErrors(o => o.Amount, o => o.ProductId)).ToList();
            var entityErrors = entity.GetValidationErrors(x => x.CodeId).ToList();
            var foundClient = Db.Clients.SingleOrDefault(x => x.Id == entity.ClientId);
            if (foundClient == null)
                errors.Add(new DbValidationError("Client", "Client must be specified"));
            if (!entity.OrderItems.Any())
                errors.Add(new DbValidationError(string.Empty, "Require to add order items"));
            errors = errors.Concat(entityErrors).ToList();
            errors.ThrowIfHasErrors();
        }
    }
}