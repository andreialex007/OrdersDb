using System.Web;
using System.Web.Mvc;
using OrdersDb.Domain.Services.Accounts.User;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Code.Extensions;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItem(Icon = "fa-male", Name = "Пользователи")]
    public class UsersController : NamedEntityControllerBase<IUserService, User, UserSearchParameters, UserDto>
    {
        public UsersController(IUserService service)
            : base(service)
        {
        }

        public JsonResult UserId()
        {
            var user = _service.GetByUserName(User.Identity.Name);
            return Json(new
                        {
                            user.Id
                        });
        }


        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file, int? id)
        {
            _service.UploadImage(x => x.Image, file.ToByteArray(), id);
            return SuccessJsonResult();
        }

        [HttpGet]
        public ActionResult GetImage(int? id)
        {
            var flagData = _service.GetImage(x => x.Image, id) ?? System.IO.File.ReadAllBytes(Server.MapPath("~/Images/default-flag.jpg"));
            return File(flagData, System.Net.Mime.MediaTypeNames.Image.Jpeg);
        }

    }
}