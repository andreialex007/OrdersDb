using System.IO;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrdersDb.Domain.Utils;

namespace OrdersDb.WebApp.Code
{
    public class JsonNetModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (!controllerContext.HttpContext.Request.ContentType.Contains("application/json"))
                return base.BindModel(controllerContext, bindingContext);

            var stream = controllerContext.HttpContext.Request.InputStream;
            stream.Position = 0;
            var jsonString = new StreamReader(stream).ReadToEnd();

            if (string.IsNullOrWhiteSpace(jsonString))
                return bindingContext.ModelType.GetDefault();

            var jObject = JObject.Parse(jsonString);
            var property = jObject.Properties().SingleOrDefault(x => x.Name == bindingContext.ModelName);

            if (!bindingContext.ModelMetadata.IsComplexType)
            {
                if (property != null)
                    return property.Value.ToString().Convert(bindingContext.ModelType);

                return jsonString.Convert(bindingContext.ModelType);
            }

            if (property != null)
                return JsonConvert.DeserializeObject(property.Value.ToString(), bindingContext.ModelType);

            return JsonConvert.DeserializeObject(jsonString, bindingContext.ModelType); ;
        }
    }
}