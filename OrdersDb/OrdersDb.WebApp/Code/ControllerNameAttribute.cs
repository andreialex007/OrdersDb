using System;

namespace OrdersDb.WebApp.Code
{
    public class ControllerNameAttribute : Attribute
    {
        public ControllerNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}