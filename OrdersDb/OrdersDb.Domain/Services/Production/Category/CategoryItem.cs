namespace OrdersDb.Domain.Services.Production.Category
{
    public class CategoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentsCount { get; set; }

        public static CategoryItem FromCategory(Category category)
        {
            var numberOfParents = 0;
            var currentCategory = category;
            while (currentCategory.ParentCategory != null)
            {
                numberOfParents++;
                currentCategory = currentCategory.ParentCategory;
            }

            return new CategoryItem
                   {
                       Id = category.Id,
                       Name = category.Name,
                       ParentsCount = numberOfParents
                   };
        }
    }
}