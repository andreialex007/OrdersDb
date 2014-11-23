using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using OrdersDb.Domain.Services.Orders.OrderItem;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Production.Product
{
    /// <summary>
    /// Продукт
    /// </summary>
    public class Product : EntityBase, INamedEntity
    {
        public Product()
        {
            OrderItems = new List<OrderItem>();
        }

        public override int Id { get; set; }

        /// <summary>
        /// Название продукта
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Цена закупки
        /// </summary>
        [Min(1)]
        public decimal BuyPrice { get; set; }

        /// <summary>
        /// Цена продажи
        /// </summary>
         [Min(1)]
        public decimal SellPrice { get; set; }

        /// <summary>
        /// Является ли услугой
        /// </summary>
        public bool IsService { get; set; }

        /// <summary>
        /// Категория к которой принадлежит данный продукт
        /// </summary>
        [Required]
        public Category.Category Category { get; set; }

        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Элементы заказа в которых присутствует данный продукт
        /// </summary>
        public List<OrderItem> OrderItems { get; set; }
    }
}
