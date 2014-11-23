using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using OrdersDb.WebApp.App_Start;
using Owin;

//[assembly: OwinStartup(typeof(OrdersDb.WebApp.App_Start.Startup))]
[assembly: OwinStartup(typeof(Startup))]
namespace OrdersDb.WebApp.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
                                        {
                                            AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                                            LoginPath = new PathString("/Account")
                                        });
        }
    }
}