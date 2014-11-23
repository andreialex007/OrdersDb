using System.Web.Mvc;
using OrdersDb.Domain.Services.Geography.Hose;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItem(Icon = "fa-home", Name = "Дома")]
    public class HousesController : ControllerBase<IHouseService, House, HouseSearchParameters, HouseDto>
    {
        public HousesController(IHouseService service)
            : base(service)
        {
        }
    }
}