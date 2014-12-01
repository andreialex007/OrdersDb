using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrdersDb.Domain.Services.Orders.OrderItem;
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
                query = query.Where(x => x.BuyPrice >= @params.MinBuyPrice);

            if (@params.MaxBuyPrice != null)
                query = query.Where(x => x.BuyPrice <= @params.MaxBuyPrice);

            if (@params.MinSellPrice != null)
                query = query.Where(x => x.SellPrice >= @params.MinSellPrice);

            if (@params.MaxSellPrice != null)
                query = query.Where(x => x.SellPrice <= @params.MaxSellPrice);

            if (!string.IsNullOrEmpty(@params.ClientName))
                query = query.Where(x => x.Client.Name.ToLower().Contains(@params.ClientName.ToLower()));

            return query.OrderByTakeSkip(@params).Select(x => new OrderDto
                                                                       {
                                                                           Id = x.Id,
                                                                           Code = x.Code.Value,
                                                                           BuyPrice = x.BuyPrice,
                                                                           ClientName = x.Client.Name,
                                                                           SellPrice = x.SellPrice,
                                                                           TotalItems = x.OrderItems.Count
                                                                       }).ToList();
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
                                     ClientName = x.Client.Name,
                                     BuyPrice = x.BuyPrice,
                                     SellPrice = x.SellPrice,
                                     ClientId = x.Client.Id,
                                     TotalItems = x.OrderItems.Count,
                                     OrderItems = x.OrderItems.Select(o => new OrderItemDto
                                                                           {
                                                                               Amount = o.Amount,
                                                                               BuyPrice = o.BuyPrice,
                                                                               Id = o.Id,
                                                                               SellPrice = o.SellPrice,
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
                .Select(x => new NameValue { Id = x.Id, Name = x.Name })
                .OrderBy(x => x.Name)
                .ToList();

            return orderDto;
        }
    }
}