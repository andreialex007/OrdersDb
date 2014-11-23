using System.Web.Mvc;
using System.Web.Routing;

namespace OrdersDb.WebApp.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "login", new { controller = "Account", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute(null, "logout", new { controller = "Account", action = "LogOut", id = UrlParameter.Optional });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Index", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
