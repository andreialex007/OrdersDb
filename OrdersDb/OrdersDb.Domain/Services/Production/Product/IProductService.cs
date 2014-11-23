using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Production.Product
{
    public interface IProductService : INamedServiceBase<Product, ProductSearchParameters, ProductDto>
    {
    }
}