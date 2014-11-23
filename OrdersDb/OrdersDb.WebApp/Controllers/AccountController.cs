using System.Web.Mvc;
using OrdersDb.Domain.Services.Accounts.User;
using OrdersDb.WebApp.Controllers._Common;
using ControllerBase = OrdersDb.WebApp.Controllers._Common.ControllerBase;

namespace OrdersDb.WebApp.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View("IndexUnauthorized");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string name, string password, bool isPersistent = false)
        {
            var user = _userService.Login(name, password);
            this.SignIn(user, isPersistent);
            return SuccessJsonResult();
        }

        public ActionResult LogOut()
        {
            this.SignOut();
            return Redirect(Url.Content("~/"));
        }
    }
}