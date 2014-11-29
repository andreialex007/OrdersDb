using System.Collections.Generic;
using OrdersDb.Domain.Services.Production.Category;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Production.Product
{
    public class ProductDto : NamedDtoBase
    {
        public decimal? BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }
        public bool IsService { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public List<CategoryItem> CategoryItems { get; set; }
    }
}
