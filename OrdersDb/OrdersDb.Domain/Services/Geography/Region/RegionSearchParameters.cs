using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Geography.Region
{
    public class RegionSearchParameters : NamedSearchParameters
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
