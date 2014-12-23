using System.Web.Mvc;
using OrdersDb.Domain.Services.Geography.Region;
using OrdersDb.Resources;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItemEntityResourceAttribute(Icon = "fa-tree")]
    public class RegionsController : NamedEntityControllerBase<IRegionService, Region, RegionSearchParameters, RegionDto>
    {
        public RegionsController(IRegionService service)
            : base(service)
        {
        }

        public ActionResult GetRegionsInCountry(int countryId)
        {
            return Json(_service.GetRegionsInCountry(countryId));
        }
    }
}