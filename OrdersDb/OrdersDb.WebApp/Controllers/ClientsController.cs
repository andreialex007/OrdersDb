using System.Web.Mvc;
using OrdersDb.Domain.Services.Production.Client;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItem(Icon = "fa-child", Name = "Клиенты")]
    public class ClientsController : ControllerBase<IClientService, Client, ClientSearchParameters, ClientDto>
    {
        public ClientsController(IClientService service)
            : base(service)
        {
        }
    }
}