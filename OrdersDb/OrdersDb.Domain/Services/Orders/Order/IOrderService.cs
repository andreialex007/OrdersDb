using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Orders.Order
{
    public interface IOrderService : IServiceBase<Order, OrderSearchParameters, OrderDto>
    {
        
    }
}