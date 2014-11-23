using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Geography.Hose
{
    public class HouseDto : DtoBase
    {
        public int Number { get; set; }
        public string Building { get; set; }
        public string PostalCode { get; set; }
        public int StreetId { get; set; }
        public string StreetName { get; set; }
    }
}
