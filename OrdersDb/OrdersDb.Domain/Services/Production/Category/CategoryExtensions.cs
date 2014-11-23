using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace OrdersDb.Domain.Services.Production.Category
{
    public static class CategoryExtensions
    {
        public static List<CategoryItem> GetCategoriesFlatList(this Wrappers.IDbSet<Category> dbSet)
        {
            var query = dbSet
               .Include(x => x.Categories.Select(c => c.Categories))
               .Include(x => x.ParentCategory)
               .Include(x => x.Products)
               .AsQueryable();

            var categories = query.Where(x => x.ParentCategory == null).ToList();
            var result = new List<CategoryItem>();
            FlattenChildren(categories, result);
            return result;
        }

        private static void FlattenChildren(IEnumerable<Category> categories, ICollection<CategoryItem> result)
        {
            foreach (var category in categories)
            {
                result.Add(CategoryItem.FromCategory(category));
                FlattenChildren(category.Categories, result);
            }
        }
    }
}
