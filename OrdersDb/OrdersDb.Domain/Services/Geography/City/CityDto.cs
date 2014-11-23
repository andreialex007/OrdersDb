using System.Collections.Generic;
using OrdersDb.Domain.Services.Geography.Region;
using OrdersDb.Domain.Services.Geography.Street;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Geography.City
{
    public class CityDto : NamedDtoBase
    {
        public int RegionId { get; set; }
        public int Population { get; set; }
        public string RegionName { get; set; }
        public RegionDto Region { get; set; }
        public List<StreetDto> Streets { get; set; }
    }
}
