namespace OrdersDb.Domain.Services.Production.Category
{
    public class CategoryInfo : Category
    {
        public int CategoriesAmount { get; set; }
        public int ProductsAmount { get; set; }
        public string ParentCategoryName { get; set; }
    }
}
