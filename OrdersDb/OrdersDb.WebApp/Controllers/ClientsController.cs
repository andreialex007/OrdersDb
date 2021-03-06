﻿using System.Web.Mvc;
using OrdersDb.Domain.Services.Production.Client;
using OrdersDb.Resources;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItemEntityResourceAttribute(Icon = "fa-child")]
    public class ClientsController : ControllerBase<IClientService, Client, ClientSearchParameters, ClientDto>
    {
        public ClientsController(IClientService service)
            : base(service)
        {
        }
    }
}