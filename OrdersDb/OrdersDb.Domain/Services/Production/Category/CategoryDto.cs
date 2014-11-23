using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Production.Category
{
    public class CategoryDto : NamedDtoBase
    {
        public string Description { get; set; }
        public bool HasImage
        {
            get
            {
                return ImageFull != null;
            }
        }
        public byte[] ImagePreview { get; set; }
        public byte[] ImageFull { get; set; }
        public int? CategoryId { get; set; }
        public string ParentCategoryName { get; set; }
        public int CategoriesAmount { get; set; }
        public int ProductsAmount { get; set; }
    }
}
