using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Production.Product
{
    public class ProductSearchParameters : NamedSearchParameters
    {
        public decimal? MinBuyPrice { get; set; }
        public decimal? MaxBuyPrice { get; set; }
        public decimal? MinSellPrice { get; set; }
        public decimal? MaxSellPrice { get; set; }
        public bool? IsService { get; set; }
        public string CategoryName { get; set; }
    }
}
