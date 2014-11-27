using OrdersDb.Domain.Services.Orders.Order;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItem(Icon = "fa-dropbox", Name = "Заказы")]
    public class OrdersController : ControllerBase<OrderService, Order, OrderSearchParameters, OrderDto>
    {
        public OrdersController(OrderService service)
            : base(service)
        {
        }
    }
}