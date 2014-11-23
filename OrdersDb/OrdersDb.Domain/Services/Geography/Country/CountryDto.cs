using System.Collections.Generic;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Geography.Country
{
    /// <summary>
    /// Страна
    /// </summary>
    public class CountryDto : NamedDtoBase
    {
        public string Code { get; set; }
        public string RussianName { get; set; }
        public byte[] Flag { get; set; }
        public List<Region.RegionDto> Regions { get; set; } 
    }
}
