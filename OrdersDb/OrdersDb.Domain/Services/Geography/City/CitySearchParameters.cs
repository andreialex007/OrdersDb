using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Geography.City
{
    public class CitySearchParameters : NamedSearchParameters
    {
        public int? MinPopulation { get; set; }
        public int? MaxPopulation { get; set; }
        public string RegionName { get; set; }
    }
}