using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using OrdersDb.Domain.Services.Orders.OrderItem;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Resources;

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
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Product_Name", ResourceType = typeof(EntitiesResources))]
        public string Name { get; set; }

        /// <summary>
        /// Цена закупки
        /// </summary>
        [Min(1, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "ValueMustBeSpecified")]
        [Display(Name = "Product_BuyPrice", ResourceType = typeof(EntitiesResources))]
        public decimal BuyPrice { get; set; }

        /// <summary>
        /// Цена продажи
        /// </summary>
        [Min(1, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "ValueMustBeSpecified")]
        [Display(Name = "Product_SellPrice", ResourceType = typeof(EntitiesResources))]
        public decimal SellPrice { get; set; }

        /// <summary>
        /// Является ли услугой
        /// </summary>
        public bool IsService { get; set; }

        /// <summary>
        /// Категория к которой принадлежит данный продукт
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Product_Category", ResourceType = typeof(EntitiesResources))]
        public Category.Category Category { get; set; }

        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Элементы заказа в которых присутствует данный продукт
        /// </summary>
        public List<OrderItem> OrderItems { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}, BuyPrice: {2}, SellPrice: {3}, IsService: {4}, Category: {5}, CategoryId: {6}", Id, Name, BuyPrice, SellPrice, IsService, Category, CategoryId);
        }
    }
}
