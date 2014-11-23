using System.Data.Entity;
using Moq;
using NUnit.Framework;
using OrdersDb.Domain.Exceptions;
using OrdersDb.Domain.Services;
using OrdersDb.Domain.Services.Geography.City;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Tests.Common;
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Domain.Tests
{
    public class ServiceBaseTests : TestsBase
    {
        [Test]
        public void Add_EntityPassed_IdGenerated()
        {
            //arragne
            City addedCity = null;
            var db = new Mock<IAppDbContext>();

            var entry = new Mock<IDbEntityEntry<City>>();
            var context = new Mock<IObjectContext>();
            var set = new Mock<Wrappers.IDbSet<City>>();
            entry.SetupGet(x => x.State).Returns(EntityState.Detached);
            db.Setup(x => x.Entry(It.IsAny<City>())).Returns(entry.Object);
            db.Setup(x => x.Set<City>()).Returns(set.Object);
            db.Setup(x => x.Entry(It.IsAny<City>()))
                .Callback((City c) =>
                          {
                              addedCity = c;
                          }).Returns(entry.Object);
            db.Setup(x => x.SaveChanges()).Callback(() => addedCity.Id = 10);
            var serviceBase = new ServiceBase<City,CityDto>(db.Object, context.Object);

            //act
            var newCity = new City { Name = "Moscow" };
            serviceBase.Add(newCity);

            //assert
            set.Verify(x => x.Attach(It.IsAny<City>()), Times.Once());
            db.Verify(x => x.SaveChanges(), Times.Once());
            Assert.True(newCity.Id > 0);
        }

        [Test]
        public void Add_InvalidEntityPassed_ThrowsValidationException()
        {
            //arragne
            var db = new Mock<IAppDbContext>();
            var context = new Mock<IObjectContext>();
            var serviceBase = new ServiceBase<City, CityDto>(db.Object, context.Object);

            //act
            var newCity = new City { Name = string.Empty };

            try
            {
                serviceBase.Add(newCity);
            }
            catch (ValidationException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void Update_InvalidEntityPassed_ThrowsValidationException()
        {
            //arragne
            var db = new Mock<IAppDbContext>();
            var context = new Mock<IObjectContext>();
            var serviceBase = new ServiceBase<City, CityDto>(db.Object, context.Object);

            //act
            var newCity = new City { Name = string.Empty };

            //assert
            try
            {
                serviceBase.Update(newCity);
                Assert.Fail();
            }
            catch (ValidationException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void Delete_IdPasssed_ObjectDeleted()
        {
            //arragne
            var db = new Mock<IAppDbContext>();
            var context = new Mock<IObjectContext>();
            var entry = new Mock<IDbEntityEntry<City>>();
            var set = new Mock<Wrappers.IDbSet<City>>();
            entry.SetupGet(x => x.State).Returns(EntityState.Detached);
            db.Setup(x => x.Entry(It.IsAny<City>())).Returns(entry.Object);
            db.Setup(x => x.Set<City>()).Returns(set.Object);
            db.Setup(x => x.Entry(It.IsAny<City>())).Returns(entry.Object);
            var serviceBase = new ServiceBase<City, CityDto>(db.Object, context.Object);

            //act
            serviceBase.Delete(10);

            //assert
            db.Verify(x => x.SaveChanges(), Times.Once());
        }
    }
}
