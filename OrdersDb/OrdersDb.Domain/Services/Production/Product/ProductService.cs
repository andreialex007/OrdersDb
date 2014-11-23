using System.Collections.Generic;
using System.Linq;
using OrdersDb.Domain.Services.Production.Category;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;
using System.Data.Entity;

namespace OrdersDb.Domain.Services.Production.Product
{
    public class ProductService : NamedServiceBase<Product, ProductSearchParameters, ProductDto>, IProductService
    {
        public ProductService(IAppDbContext db, IObjectContext context)
            : base(db, context)
        {
        }

        public override ProductDto GetById(int id)
        {
            var query = Db.Products.AsQueryable();

            var productDto = new ProductDto();
            if (id != 0)
                productDto = query
                .Where(x => x.Id == id)
                .Select(x => new ProductDto
                                               {
                                                   Id = x.Id,
                                                   Name = x.Name,
                                                   BuyPrice = x.BuyPrice,
                                                   CategoryId = x.CategoryId,
                                                   CategoryName = x.Category.Name,
                                                   IsService = x.IsService,
                                                   SellPrice = x.SellPrice
                                               }).Single();

            productDto.CategoryItems = Db.Categories.GetCategoriesFlatList();
            return productDto;
        }

        public override List<ProductDto> Search(ProductSearchParameters @params)
        {
            var query = Db.Set<Product>()
                .Include(x => x.Category)
                .AsQueryable();

            query = SearchByIds(query, @params);
            query = SearchByName(query, @params);

            if (@params.MinBuyPrice != null)
                query = query.Where(x => x.BuyPrice >= @params.MinBuyPrice);

            if (@params.MaxBuyPrice != null)
                query = query.Where(x => x.BuyPrice <= @params.MaxBuyPrice);

            if (@params.MinSellPrice != null)
                query = query.Where(x => x.SellPrice >= @params.MinSellPrice);

            if (@params.MaxSellPrice != null)
                query = query.Where(x => x.SellPrice <= @params.MaxSellPrice);

            if (@params.IsService.HasValue)
                query = query.Where(x => x.IsService == @params.IsService);

            if (!string.IsNullOrEmpty(@params.CategoryName))
                query = query.Where(x => x.Category.Name.ToLower().Contains(@params.CategoryName.ToLower()));

            return query.OrderByTakeSkip(@params).Select(x => new ProductDto
                                                              {
                                                                  Id = x.Id,
                                                                  Name = x.Name,
                                                                  BuyPrice = x.BuyPrice,
                                                                  SellPrice = x.SellPrice,
                                                                  IsService = x.IsService,
                                                                  CategoryId = x.CategoryId,
                                                                  CategoryName =
                                                                      x.Category != null ? x.Category.Name : string.Empty
                                                              }).ToList();
        }
    }
}
