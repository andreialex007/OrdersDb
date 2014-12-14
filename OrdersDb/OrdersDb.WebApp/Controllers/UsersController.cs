using System.Web.Mvc;
using OrdersDb.Domain.Services.Accounts.User;
using OrdersDb.WebApp.Code;
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
    }
}