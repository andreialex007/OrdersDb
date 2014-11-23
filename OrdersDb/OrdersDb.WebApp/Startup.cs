using Microsoft.Owin;
using OrdersDb.WebApp.App_Start;
using Owin;

[assembly: OwinStartupAttribute(typeof(Startup))]
namespace OrdersDb.WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
