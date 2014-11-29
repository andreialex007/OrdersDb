using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Orders.OrderItem
{
    public class OrderItemDto : DtoBase
    {
        public int Amount { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public decimal SellPrice { get; set; }
        public decimal BuyPrice { get; set; }
    }
}
