using System.Collections.Generic;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Geography.Region
{
    public interface IRegionService : INamedServiceBase<Region, RegionSearchParameters, RegionDto>
    {
        void AddRegionWithCities(Region region);

        /// <summary>
        /// Возвращает регион с городами
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        List<Region> GetRegionsWithCities(int take, int skip);

        List<NameValue> GetRegionsInCountry(int countryId);
    }
}