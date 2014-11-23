using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrdersDb.WebApp.Code
{
    public class MenuItemAttribute : Attribute
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}