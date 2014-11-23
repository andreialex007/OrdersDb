using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Staff.Position
{
    public interface IPositionService : INamedServiceBase<Position, NamedSearchParameters, PositionDto>
    {
    }
}