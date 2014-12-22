using System.Web.Mvc;
using OrdersDb.Domain.Services.Geography.Street;
using OrdersDb.Resources;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItemEntityResourceAttribute(Icon = "fa-road")]
    public class StreetsController : NamedEntityControllerBase<IStreetService, Street, StreetSearchParameters, StreetDto>
    {
        public StreetsController(IStreetService service)
            : base(service)
        {
        }

        public ActionResult GetStreetsByCity(int cityId)
        {
            return Json(_service.GetStreetsByCity(cityId));
        }
    }
}