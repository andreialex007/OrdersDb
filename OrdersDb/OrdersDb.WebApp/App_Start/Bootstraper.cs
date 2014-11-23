using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using OrdersDb.Data;
using OrdersDb.Domain.Services.SystemServices;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Pipeline;
using StructureMap.Web.Pipeline;

// ReSharper disable  CSharpWarnings::CS0618

namespace OrdersDb.WebApp.App_Start
{
    public class Bootstraper
    {
        public static void Bootstrap()
        {
            var assembliesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            ObjectFactory.Configure(config =>
            {
                config.For<IAppDbContext>().LifecycleIs<HttpContextLifecycle>().Use<AppDbContextWrapper>();
                config.For<IObjectContext>()
                    .LifecycleIs<HttpContextLifecycle>()
                    .Use(() => new ObjectContextWrapper(ObjectFactory.GetInstance<IAppDbContext>().ObjectContext));
                config.For(typeof(IServiceBase<,,>)).Use(typeof(ServiceBase<,,>));
                config.For(typeof(IFileService)).Use(typeof(FileServiceWrapper));
                config.Scan(
                    x =>
                    {
                        x.ConnectImplementationsToTypesClosing(typeof(IServiceBase<,>));
                        x.AssembliesFromPath(assembliesPath);
                        x.WithDefaultConventions()
                            .OnAddedPluginTypes(t => t.LifecycleIs(Lifecycles.Get<HttpContextLifecycle>()));

                    });
            });

            IoC.ResolvingExpression = ObjectFactory.GetInstance;
        }
    }


    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)ObjectFactory.GetInstance(controllerType);
        }
    }
}