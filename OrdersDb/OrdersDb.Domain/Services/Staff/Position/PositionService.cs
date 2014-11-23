using System.Linq;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Domain.Services.Staff.Position
{
    public class PositionService : NamedServiceBase<Position, NamedSearchParameters, PositionDto>, IPositionService
    {
        public PositionService(IAppDbContext db, IObjectContext context)
            : base(db, context)
        {
        }

        public override PositionDto GetById(int id)
        {
            var positions = Db.Positions.AsQueryable();

            var positoinDto = positions.Where(x => x.Id == id)
                .Select(x => new PositionDto { Id = x.Id, Name = x.Name })
                .Single();

            return positoinDto;
        }
    }
}
