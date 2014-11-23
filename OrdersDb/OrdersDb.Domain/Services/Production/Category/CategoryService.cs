using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Domain.Services.Production.Category
{
    public class CategoryService : NamedServiceBase<Category, CategorySearchParameters, CategoryDto>, ICategoryService
    {
        private const string NewCategoryName = "Новая категория";
        private const int PreviewImageSideSize = 100;

        public CategoryService(IAppDbContext db, IObjectContext context)
            : base(db, context)
        {
        }

        public override List<CategoryDto> Search(CategorySearchParameters @params)
        {
            var query = Db.Set<Category>()
               .Include(x => x.Categories)
               .Include(x => x.ParentCategory)
               .Include(x => x.Products)
               .AsQueryable();

            query = SearchByIds(query, @params);
            query = SearchByName(query, @params);

            if (!string.IsNullOrEmpty(@params.ParentCategoryName))
                query = query.Where(x => x.ParentCategory.Name.ToLower().Contains(@params.ParentCategoryName));

            if (@params.ParentCategoryFilterEnabled)
                query = query.Where(x => x.CategoryId == @params.ParentCategoryId);

            return query.OrderByTakeSkip(@params).Select(x => new CategoryDto
                                                              {
                                                                  Id = x.Id,
                                                                  Name = x.Name,
                                                                  ParentCategoryName = x.ParentCategory == null ? string.Empty : x.ParentCategory.Name,
                                                                  CategoriesAmount = x.Categories.Count,
                                                                  ProductsAmount = x.Products.Count
                                                              }).ToList();
        }

        public List<CategoryItem> GetFlatList()
        {
            return Db.Set<Category>().GetCategoriesFlatList();
        }

        

        public void SaveImage(int categoryId, byte[] imageData)
        {
            //Конвертируем полное изобржение
            var fullImage = ImageUtils.ConvertToJpg(imageData);

            //Конвертируе превью
            using (var bitmap = imageData.ToBitmap())
            {
                var resized = ImageUtils.ResizeImage(bitmap, new Size(PreviewImageSideSize, PreviewImageSideSize));
                var previewImage = resized.ToByteArray(ImageFormat.Jpeg);
                var category = new Category { Id = categoryId, ImagePreview = previewImage, ImageFull = fullImage };
                Db.Categories.Attach(category);
                Db.Entry(category).Property(x => x.ImagePreview).IsModified = true;
                Db.Entry(category).Property(x => x.ImageFull).IsModified = true;
                Db.SaveChanges();
            }
        }

        public void SaveCategoryInfo(Category category)
        {
            var errors = category.GetValidationErrors(x => x.Name, x => x.Description);
            errors.ThrowIfHasErrors();
            Db.AttachIfDetached(category);
            Db.Entry(category).Property(x => x.Name).IsModified = true;
            Db.Entry(category).Property(x => x.Description).IsModified = true;
            Db.SaveChanges();
        }

        public Category AddNewCategory(int? parentCategoryId = null)
        {
            var category = new Category { Name = NewCategoryName };
            if (parentCategoryId.HasValue)
                category.CategoryId = parentCategoryId;
            Db.Categories.Add(category);
            Db.SaveChanges();
            return category;
        }

        public CategoryInfo GetCategoryInfoById(int id)
        {
            return Db.Set<Category>()
                .IncludeAll()
                .Where(x => x.Id == id)
                .Select(x => new
                          {
                              x.Id,
                              x.Name,
                              x.Description,
                              x.ImagePreview,
                              x.ImageFull,
                              ParentCategoryName = x.ParentCategory == null ? string.Empty : x.ParentCategory.Name,
                              ProductsAmount = x.Products.
                                  Count,
                              CategoriesAmount = x.Categories.Count,
                              x.Created,
                              x.Modified
                          })
                .To<CategoryInfo>()
                .Single();
        }

        public override void Delete(int id)
        {
            DeleteCategories(new[] { id });
        }

        private void DeleteCategories(IEnumerable<int> parentIds)
        {
            var categoriesToDelete = Db.Categories
                .Include(x => x.Categories)
                .Where(x => parentIds.Contains(x.Id))
                .ToList();

            var childrenCategoryIds = categoriesToDelete
                .SelectMany(x => x.Categories.Select(c => c.Id))
                .ToList();

            if (childrenCategoryIds.Any())
                DeleteCategories(childrenCategoryIds);

            Db.Categories.RemoveRange(categoriesToDelete);
            Db.SaveChanges();
        }

        public byte[] GetImagePreview(int id)
        {
            return Db.Categories.Where(x => x.Id == id).Select(x => x.ImagePreview).First();
        }

        public byte[] GetImageFull(int id)
        {
            return Db.Categories.Where(x => x.Id == id).Select(x => x.ImageFull).First();
        }
    }
}
