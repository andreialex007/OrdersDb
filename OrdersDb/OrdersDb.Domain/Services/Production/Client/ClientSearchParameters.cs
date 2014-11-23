using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Production.Client
{
    public class ClientSearchParameters : NamedSearchParameters
    {
        public string FullName { get; set; }
        public string INN { get; set; }
        public string OGRN { get; set; }
        public string LocationString { get; set; }
    }
}
