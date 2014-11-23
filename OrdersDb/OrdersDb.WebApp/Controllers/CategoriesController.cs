using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersDb.Domain.Services.Production.Category;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Code.Extensions;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItem(Icon = "fa-folder", Name = "Категории товаров")]
    public class CategoriesController : ControllerBase<ICategoryService, Category, CategorySearchParameters, CategoryDto>
    {
        public CategoriesController(ICategoryService service)
            : base(service)
        {
        }

        [HttpPost]
        [AccessType(AccessType.Update)]
        public ActionResult UploadImage(HttpPostedFileBase file, int id)
        {
            _service.SaveImage(id, file.ToByteArray());
            return SuccessJsonResult();
        }

        [HttpGet]
        public ActionResult ShowImagePreview(int id)
        {
            return File(_service.GetImagePreview(id), System.Net.Mime.MediaTypeNames.Image.Jpeg);
        }

        [HttpGet]
        public ActionResult ShowImageFull(int id)
        {
            return File(_service.GetImageFull(id), System.Net.Mime.MediaTypeNames.Image.Jpeg);
        }

        public override ActionResult GetById(int id)
        {
            return Json(_service.GetCategoryInfoById(id));
        }

        [AccessType(AccessType.Update)]
        public ActionResult SaveInfo(Category category)
        {
            _service.SaveCategoryInfo(category);
            return SuccessJsonResult();
        }

        [AccessType(AccessType.Add)]
        public ActionResult AddNewCategory(int? parentCategoryId)
        {
            var category = _service.AddNewCategory(parentCategoryId);
            return Json(new { category.Name, category.Id });
        }
    }
}