using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Orders.Order
{
    public class OrderSearchParameters : SearchParameters
    {
        public string Code { get; set; }
        public string ClientName { get; set; }

        public int? MinSellPrice { get; set; }
        public int? MaxSellPrice { get; set; }

        public int? MinBuyPrice { get; set; }
        public int? MaxBuyPrice { get; set; }
    }
}
