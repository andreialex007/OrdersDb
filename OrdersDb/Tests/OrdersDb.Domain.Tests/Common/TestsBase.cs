using System;
using System.Data.Entity;
using NUnit.Framework;
using OrdersDb.Data;
using OrdersDb.Domain.Wrappers;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Pipeline;

namespace OrdersDb.Domain.Tests.Common
{
    [TestFixture]
    public class TestsBase
    {
        protected IAppDbContext Db;

        [SetUp]
        public virtual void Setup()
        {
            Bootstrap();
        }

        public virtual void TearDown()
        {

        }

        public void Bootstrap()
        {
            ObjectFactory.Configure(config =>
            {
                config.For<DbContext>().LifecycleIs<SingletonLifecycle>().Use(new AppDbContext());
                config.For<IAppDbContext>().LifecycleIs<SingletonLifecycle>().Use<AppDbContextWrapper>();
                config.For(typeof(Wrappers.IDbSet<>)).LifecycleIs(Lifecycles.Singleton).Use(typeof(DbSetWrapper<>));
                config.For(typeof(IDbEntityEntry<>)).LifecycleIs(Lifecycles.Singleton).Use(typeof(DbEntityEntryWrapper<>));
                config.Scan(
                    x =>
                    {
                        x.AssembliesFromPath(Environment.CurrentDirectory);
                        x.WithDefaultConventions()
                            .OnAddedPluginTypes(t => t.LifecycleIs(Lifecycles.Get<SingletonLifecycle>()));
                    });
            });

            Db = ObjectFactory.GetInstance<IAppDbContext>();
        }

    }
}
