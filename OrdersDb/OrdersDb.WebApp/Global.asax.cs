using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OrdersDb.WebApp.App_Start;
using OrdersDb.WebApp.Code;
using StructureMap.Web.Pipeline;

namespace OrdersDb.WebApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MapperConfig.Config();
            ModelBinders.Binders.DefaultBinder = new JsonNetModelBinder();
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            Bootstraper.Bootstrap();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            HttpContextLifecycle.DisposeAndClearAll();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //You don't want to redirect on posts, or images/css/js
            var isGet = HttpContext.Current.Request.RequestType.ToLowerInvariant().Contains("get");
            if (isGet && HttpContext.Current.Request.Url.AbsolutePath.Contains(".") == false)
            {
                var lowercaseURL = (Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority +
                                    HttpContext.Current.Request.Url.AbsolutePath);
                if (!Regex.IsMatch(lowercaseURL, @"[A-Z]")) return;
                //You don't want to change casing on query strings
                lowercaseURL = lowercaseURL.ToLower() + HttpContext.Current.Request.Url.Query;

                Response.Clear();
                Response.Status = "301 Moved Permanently";
                Response.AddHeader("Location", lowercaseURL);
                Response.End();
            }
        }
    }
}
