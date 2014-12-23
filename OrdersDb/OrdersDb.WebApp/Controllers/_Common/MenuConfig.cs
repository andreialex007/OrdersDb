using System.Collections.Generic;
using System.Linq;
using OrdersDb.Domain.Utils;
using OrdersDb.Resources;
using OrdersDb.WebApp.Code;

namespace OrdersDb.WebApp.Controllers._Common
{
    public static class MenuConfig
    {
        public static MenuItem From<TControllerType>() where TControllerType : ControllerBase
        {
            var type = typeof(TControllerType);
            var customAttributes = type.GetCustomAttributes(typeof(MenuItemAttribute), false);
            if (customAttributes.Any())
            {
                var menuitem = new MenuItem();
                var menuItemAttr = (MenuItemAttribute)customAttributes.First();
                var id = menuItemAttr.Id ?? type.Name.Replace("Controller", string.Empty);
                menuitem.Id = id.ToLower();
                menuItemAttr.ResourcePropertyName = id;
                menuitem.Name = menuItemAttr.Name ?? menuitem.Id;
                menuitem.Icon = menuItemAttr.Icon ?? "fa-circle";
                return menuitem;
            }
            return null;
        }

        public static List<MenuItem> Menu
        {
            get
            {
                return new List<MenuItem>
                   {
                       new MenuItem
                       {
                           Id = "OrdersRoot",
                           Name = CommonResources.Orders,
                           Icon = "fa-dropbox",
                           MenuItems = new List<MenuItem>
                                       {
                                           From<OrdersController>()
                                       }
                       },
                       new MenuItem
                       {
                           Id = "ProductionRoot",
                           Name = CommonResources.Production,
                           Icon = "fa-shopping-cart",
                           MenuItems = new List<MenuItem>
                                       {
                                           From<CategoriesController>(),
                                           From<ClientsController>(),
                                           From<ProductsController>(),
                                       }
                       },
                       new MenuItem
                       {
                           Id = "StaffRoot",
                           Name = CommonResources.Staff,
                           Icon = "fa-wechat",
                           MenuItems = new List<MenuItem>
                                       {
                                           From<EmployeesController>(),
                                           From<PositionsController>()
                                       }
                       },
                       new MenuItem
                       {
                           Id = "GeographyRoot",
                           Name = CommonResources.Geography,
                           Icon = "fa-globe",
                           MenuItems = new List<MenuItem>
                                       {
                                           From<CitiesController>(),
                                           From<CountriesController>(),
                                           From<HousesController>(),
                                           From<RegionsController>(),
                                           From<StreetsController>()
                                       }
                       },
                       new MenuItem
                       {
                           Id = "AdministrationRoot",
                           Name = CommonResources.Administration,
                           Icon = "fa-users",
                           MenuItems = new List<MenuItem>
                                       {
                                           From<UsersController>(),
                                           From<RolesController>()
                                       }
                       }
                   };
            }
        }
    }
}