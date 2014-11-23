using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Geography.Hose
{
    public class HouseSearchParameters : SearchParameters
    {
        public int[] Numbers { get; set; }
        public string Building { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
    }
}
