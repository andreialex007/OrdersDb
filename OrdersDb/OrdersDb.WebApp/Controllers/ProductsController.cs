using System.Web.Mvc;
using OrdersDb.Domain.Services.Production.Product;
using OrdersDb.Resources;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItemEntityResourceAttribute(Icon = "fa-cube")]
    public class ProductsController : NamedEntityControllerBase<IProductService, Product, ProductSearchParameters, ProductDto>
    {
        public ProductsController(IProductService service)
            : base(service)
        {
        }
    }
}