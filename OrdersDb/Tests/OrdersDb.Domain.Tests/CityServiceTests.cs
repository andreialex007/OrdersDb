using System.Collections.Generic;
using System.Linq;
using AutoMapper.Impl;
using Moq;
using NUnit.Framework;
using OrdersDb.Domain.Services.Geography.City;
using OrdersDb.Domain.Services.Production.Category;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Domain.Tests.Common;
using System.Data.Entity;
using OrdersDb.Domain.Wrappers;
using OrdersDb.Domain.Utils;

namespace OrdersDb.Domain.Tests
{
    [TestFixture]
    public class CityServiceTests : TestsBase
    {
        [Test]
        public void IntegrationTest()
        {
            var query = AppDbContext.Set<Category>()
               .Include(x => x.Categories.Select(c => c.Categories))
               .Include(x => x.ParentCategory)
               .Include(x => x.Products)
               .AsQueryable();

            var categories = query.Where(x => x.ParentCategory == null).ToList();
            var result = new List<CategoryItem>();
            FlattenChildren(categories, result);

//            var flatten = categories.Flatten(x => x.Categories);


            //
            //            var city = new City();
            //            //            var forProperties = city.get(x => x.Name, x => x.Id);
            //
            //            var cities = AppDbContext.Cities.Include(x => x.Region)
            //                .Include(x => x.Region)
            //                .Where(x => !string.IsNullOrEmpty(x.Name))
            //                //                .OrderBy(x => x.Name)
            //                //                .Skip(100)
            //                //                .Take(5)
            //                .ToList();
            //
            //
            //            var names = typeof(Category).GetProperties()
            //                .Select(x => new { Type = x.PropertyType, x.Name })
            //                .Where(x => typeof(EntityBase).IsAssignableFrom(x.Type) || typeof(IEnumerable<EntityBase>).IsAssignableFrom(x.Type))
            //                .Select(x => x.Name)
            //                .ToList();
            //
            //            var list = cities.OrderBy(x => x.Name).ToList();
            //            var list2 = cities.OrderByDescending(x => x.Name).ToList();
            //
            //            var orderBy = cities.AsQueryable().OrderBy(x => x.Name, false).ToList();
            //            var orderBy2 = cities.AsQueryable().OrderBy(x => x.Name, true).ToList();


            //                .ConvertAll(Mapper.Map<CityViewModel>);
        }

        public static void FlattenChildren(List<Category> categories, List<CategoryItem> result)
        {
            foreach (var category in categories)
            {
                result.Add(CategoryItem.FromCategory(category));
                FlattenChildren(category.Categories, result);
            }
        }

        private readonly City _nishnevartovsk = new City { Id = 73, Name = "Нижневартовск", RegionId = 34, Population = 251694 };
        private List<City> Cities;


        public override void Setup()
        {
            base.Setup();
            Cities = new List<City>
                     {
                         new City {Id = 74, Name = "Йошкар-Ола", RegionId = 6, Population = 248782},
                         _nishnevartovsk,
                         new City {Id = 75, Name = "Братск", RegionId = 22, Population = 246319},
                     };
        }


        [Test]
        public void Search_ParamsPassed_SearchReturnedRequiredCity()
        {
            //arragne
            var db = new Mock<IAppDbContext>();
            var context = new Mock<IObjectContext>();
            var set = new Mock<Wrappers.IDbSet<City>>();

            set.SetupIQueryable(Cities.AsQueryable());
            db.Setup(x => x.Set<City>()).Returns(set.Object);
            var cityService = new CityService(db.Object, context.Object);

            //act
            var search = cityService.Search(new CitySearchParameters { Ids = new[] { 73, 74 }, MinPopulation = 100000, Name = "нижне" }).ToList();

            //assert
            Assert.IsTrue(search.Count() == 1);
            Assert.IsTrue(search[0].Name == _nishnevartovsk.Name);
            Assert.IsTrue(search[0].Id == _nishnevartovsk.Id);
        }


        [Test]
        public void Search_ParamsPassed_SearchReturnedCorrectOrderApplied()
        {
            //arragne
            var db = new Mock<IAppDbContext>();
            var context = new Mock<IObjectContext>();
            var set = new Mock<Wrappers.IDbSet<City>>();
            set.SetupIQueryable(Cities.AsQueryable());
            db.Setup(x => x.Set<City>()).Returns(set.Object);
            var cityService = new CityService(db.Object, context.Object);

            //act
            var search = cityService.Search(new CitySearchParameters { OrderBy = "Name", IsAsc = false, Skip = 1, Take = 2 }).ToList();

            //assert
            Assert.IsTrue(search.Count == 2);
            Assert.IsTrue(search[0].Name == "Йошкар-Ола");
            Assert.IsTrue(search[1].Name == "Братск");
        }
    }
}
