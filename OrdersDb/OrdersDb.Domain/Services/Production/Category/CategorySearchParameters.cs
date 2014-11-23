using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Production.Category
{
    public class CategorySearchParameters : NamedSearchParameters
    {
        public string ParentCategoryName { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool ParentCategoryFilterEnabled { get; set; }
    }
}
