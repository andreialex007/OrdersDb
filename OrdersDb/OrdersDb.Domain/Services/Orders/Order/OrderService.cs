using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Domain.Services.Orders.Order
{
    public class OrderService : ServiceBase<Order, OrderSearchParameters, OrderDto>, IOrderService
    {
        public OrderService(IAppDbContext db, IObjectContext objectContext)
            : base(db, objectContext)
        {
        }

        public override List<OrderDto> Search(OrderSearchParameters @params)
        {
            var query = Db.Set<Order>()
                .Include(x => x.Code)
                .AsQueryable();

            query = SearchByIds(query, @params);

            if (!string.IsNullOrEmpty(@params.Code))
                query = query.Where(x => x.Code.Value.ToLower().Contains(@params.Code.ToLower()));

            return query.OrderByTakeSkip(@params).Select(x => new OrderDto
                                                  {
                                                      Id = x.Id,
                                                      Code = x.Code.Value
                                                  }).ToList();
        }

        public override OrderDto GetById(int id)
        {
            return Db.Set<Order>()
                .Include(x => x.OrderItems)
                .Include(x => x.Code)
                .Where(x => x.Id == id)
                .Select(x => new OrderDto
                             {
                                 Id = x.Id,
                                 Code = x.Code.Value
                             }).Single();
        }
    }
}