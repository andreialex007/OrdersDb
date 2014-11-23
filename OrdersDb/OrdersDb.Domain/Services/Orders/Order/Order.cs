using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OrdersDb.Domain.Services.SystemServices;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Orders.Order
{
    public class Order : EntityBase
    {
        public Order()
        {
            OrderItems = new List<OrderItem.OrderItem>();
        }

        public int CodeId { get; set; }
        /// <summary>
        /// Уникальный код заказа в базе
        /// </summary>
        [Required]
        public Code Code { get; set; }
        
        /// <summary>
        /// Идентификатор заказа в базе
        /// </summary>
        public override int Id { get; set; }

        public List<OrderItem.OrderItem> OrderItems { get; set; }
    }
}
