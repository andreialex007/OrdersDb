using Newtonsoft.Json;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Domain.Utils;

namespace OrdersDb.WebApp.Controllers._Common
{
    public class PermissionViewModel
    {
        public string Name { get; set; }
        public AccessType AccessType { get; set; }

        public string AccessTypeName { get; set; }

        public string Code { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Checked { get; set; }
    }
}