using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Resources;

namespace OrdersDb.Domain.Services.Production.Category
{
    /// <summary>
    /// Категория продукта
    /// </summary>
    public class Category : EntityBase, INamedEntity
    {
        public Category()
        {
            Categories = new List<Category>();
            Products = new List<Product.Product>();
        }

        public override int Id { get; set; }

        /// <summary>
        /// Имя категории
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        /// <summary>
        /// Описание категории
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Флаг имеет ли категория изображение
        /// </summary>
        public bool HasImage
        {
            get
            {
                return ImageFull != null;
            }
        }

        /// <summary>
        /// Превью изображения категории
        /// </summary>
        public byte[] ImagePreview { get; set; }

        /// <summary>
        /// Полное изображенине категории
        /// </summary>
        public byte[] ImageFull { get; set; }

        /// <summary>
        /// Список продуктов находящихся в этой категории
        /// </summary>
        public virtual List<Product.Product> Products { get; set; }

        /// <summary>
        /// Идентификатор родительской категории
        /// </summary>
        [ForeignKey("ParentCategory")]
        public int? CategoryId { get; set; }

        /// <summary>
        /// Родительская категория в которую входит текущая категория
        /// </summary>
        public Category ParentCategory { get; set; }

        /// <summary>
        /// Дочерние категории которые входят в текущую категорию
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public virtual List<Category> Categories { get; set; }

    }
}
