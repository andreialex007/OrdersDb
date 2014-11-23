using System.Collections.Generic;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Geography.Street
{
    public interface IStreetService : INamedServiceBase<Street, StreetSearchParameters, StreetDto>
    {
        List<NameValue> GetStreetsByCity(int cityId);
    }
}