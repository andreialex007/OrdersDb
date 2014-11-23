using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Geography.Hose
{
    public interface IHouseService : IServiceBase<House, HouseSearchParameters, HouseDto>
    {
    }
}