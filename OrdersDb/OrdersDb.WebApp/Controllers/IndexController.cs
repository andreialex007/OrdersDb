using System.Web.Mvc;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.WebApp.Controllers
{
    public class IndexController : _Common.ControllerBase
    {
        [AccessType(AccessType.Update)]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}