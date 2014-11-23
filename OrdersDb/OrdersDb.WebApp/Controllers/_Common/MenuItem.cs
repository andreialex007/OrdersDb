using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrdersDb.WebApp.Controllers._Common
{
    public class MenuItem
    {
        public MenuItem()
        {
            MenuItems = new List<MenuItem>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public bool Visible
        {
            get
            {
                if (MenuItems.Any())
                {
                    return MenuItems.Any(x => x.Visible);
                }
                var permissions = HttpContext.Current.User.GetPermissions();
                return permissions.Reads.Contains(Id);
            }
        }

        public List<MenuItem> MenuItems { get; set; }


    }
}