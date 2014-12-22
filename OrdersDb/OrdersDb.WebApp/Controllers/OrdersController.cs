using OrdersDb.Domain.Services.Orders.Order;
using OrdersDb.Resources;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItemEntityResourceAttribute(Icon = "fa-dropbox")]
    public class OrdersController : ControllerBase<OrderService, Order, OrderSearchParameters, OrderDto>
    {
        public OrdersController(OrderService service)
            : base(service)
        {
        }
    }
}