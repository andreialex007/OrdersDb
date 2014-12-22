using System.Web.Mvc;
using OrdersDb.Domain.Services.Staff.Position;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Resources;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItemEntityResourceAttribute(Icon = "fa-slideshare")]
    public class PositionsController : NamedEntityControllerBase<IPositionService, Position, NamedSearchParameters, PositionDto>
    {
        public PositionsController(IPositionService service)
            : base(service)
        {
        }
    }
}