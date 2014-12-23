using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrdersDb.Domain.Services.Production.Product;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Resources;

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
        /// Идентификатор услуги
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public int ProductId { get; set; }

        /// <summary>
        /// Продукт в данном элементе заказа
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public Product Product { get; set; }

        /// <summary>
        /// Идентификатор заказа которому принадлежит элемент
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Заказ в который входит данный элемент
        /// </summary>
        public Order.Order Order { get; set; }

        /// <summary>
        /// Цена продажи
        /// </summary>
        [NotMapped]
        public decimal SellPrice
        {
            get { return Product == null ? 0 : Amount * Product.SellPrice; }
            private set { }
        }

        /// <summary>
        /// Цена покупки
        /// </summary>
        [NotMapped]
        public decimal BuyPrice
        {
            get { return Product == null ? 0 : Amount * Product.BuyPrice; }
            private set { }
        }

        public override string ToString()
        {
            return string.Format("Id: {0}, Amount: {1}, ProductId: {2}, Product: {3}, OrderId: {4}, Order: {5}, SellPrice: {6}, BuyPrice: {7}", Id, Amount, ProductId, Product, OrderId, Order, SellPrice, BuyPrice);
        }
    }
}
