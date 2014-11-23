using System.Collections.Generic;
using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Production.Category
{
    public interface ICategoryService : IServiceBase<Category, CategorySearchParameters, CategoryDto>
    {
        void SaveImage(int categoryId, byte[] imageData);
        byte[] GetImagePreview(int id);
        byte[] GetImageFull(int id);
        CategoryInfo GetCategoryInfoById(int id);
        void SaveCategoryInfo(Category category);
        Category AddNewCategory(int? parentCategoryId = null);
        List<CategoryItem> GetFlatList();
    }
}