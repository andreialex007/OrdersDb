using System.ComponentModel.DataAnnotations;
using OrdersDb.Domain.Services.Production.Product;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Orders.OrderItem
{
    /// <summary>
    /// Элемент заказа
    /// </summary>
    public class OrderItem : EntityBase
    {
        public override int Id { get; set; }

        /// <summary>
        /// Количество продуктов в элементе заказа
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Продукт в данном элементе заказа
        /// </summary>
        [Required]
        public Product Product { get; set; }

        /// <summary>
        /// Идентификатор заказа которому принадлежит элемент
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Заказ в который входит данный элемент
        /// </summary>
        public Order.Order Order { get; set; }
    }
}
