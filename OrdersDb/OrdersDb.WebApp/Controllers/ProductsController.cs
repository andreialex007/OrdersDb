using System.Web.Mvc;
using OrdersDb.Domain.Services.Production.Product;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItem(Icon = "fa-cube", Name = "Товары")]
    public class ProductsController : NamedEntityControllerBase<IProductService, Product, ProductSearchParameters, ProductDto>
    {
        public ProductsController(IProductService service)
            : base(service)
        {
        }
    }
}