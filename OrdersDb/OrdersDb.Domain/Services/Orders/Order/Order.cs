using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using OrdersDb.Domain.Services.Production.Client;
using OrdersDb.Domain.Services.SystemServices;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Orders.Order
{
    /// <summary>
    /// Главная бизнес сущность заказ
    /// </summary>
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

        /// <summary>
        /// Позиции ( элементы ) заказа
        /// </summary>
        public List<OrderItem.OrderItem> OrderItems { get; set; }

        /// <summary>
        /// Клиент который купил данный заказ
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Цена продажи
        /// </summary>
        public decimal SellPrice
        {
            get { return OrderItems.Sum(x => x.SellPrice); }
            private set { }
        }

        /// <summary>
        /// Цена покупки
        /// </summary>
        public decimal BuyPrice
        {
            get { return OrderItems.Sum(x => x.BuyPrice); }
            private set { }
        }
    }
}
