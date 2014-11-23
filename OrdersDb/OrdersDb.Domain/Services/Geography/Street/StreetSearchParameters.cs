using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Geography.Street
{
    public class StreetSearchParameters : NamedSearchParameters
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}
