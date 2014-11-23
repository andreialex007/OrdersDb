using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Geography.Country
{
    public class CountrySearchParameters : NamedSearchParameters
    {
        public string Code { get; set; }
        public string RussianName { get; set; }
    }
}
