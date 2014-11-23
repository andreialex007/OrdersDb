using System.Web.Mvc;
using OrdersDb.Domain.Services.Geography.Street;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItem(Icon = "fa-road", Name = "Улицы")]
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