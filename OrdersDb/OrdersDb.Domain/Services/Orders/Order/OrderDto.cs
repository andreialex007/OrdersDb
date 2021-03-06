﻿using System.Collections.Generic;
using System.Linq;
using OrdersDb.Domain.Services.Orders.OrderItem;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Orders.Order
{
    public class OrderDto : DtoBase
    {
        public OrderDto()
        {
            OrderItems = new List<OrderItemDto>();
            Clients = new List<NameValue>();
        }

        public string Code { get; set; }
        public int CodeId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }

        public decimal? BuyPrice
        {
            get { return OrderItems.Sum(x => x.BuyPrice); }
        }

        public decimal? SellPrice
        {
            get { return OrderItems.Sum(x => x.SellPrice); }
        }

        public int TotalItems { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public List<ProductPriceDto> Products { get; set; }
        public List<NameValue> Clients { get; set; }
    }

    public class ProductPriceDto : NameValue
    {
        public decimal SellPrice { get; set; }
        public decimal BuyPrice { get; set; }
    }
}
