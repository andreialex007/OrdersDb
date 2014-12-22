using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Web;
using OrdersDb.Resources;

namespace OrdersDb.WebApp.Code
{
    public class MenuItemAttribute : Attribute
    {
        private string _name;
        public string Id { get; set; }

        public string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(_name))
                    return _name;

                if (ResourceType == null || string.IsNullOrEmpty(ResourcePropertyName))
                    return string.Empty;

                var manager = new ResourceManager(ResourceType);
                return manager.GetString(ResourcePropertyName);
            }
            set { _name = value; }
        }

        public string Icon { get; set; }
        public Type ResourceType { get; set; }
        public string ResourcePropertyName { get; set; }
    }

    public class MenuItemEntityResourceAttribute : MenuItemAttribute
    {
        public MenuItemEntityResourceAttribute()
        {
            this.ResourceType = typeof(EntitiesResources);
        }
    }
}