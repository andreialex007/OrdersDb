using OrdersDb.Domain.Services.Production.Category;

namespace OrdersDb.Domain.Services._Common.Entities
{
    public static class RecursiveRemoverExtension
    {
        public static void RemoveChildrenCategories(this Category category)
        {
            category.Categories.ForEach(x=>x.RemoveChildrenCategories());
            category.Categories.Clear();
        }
    }
}
