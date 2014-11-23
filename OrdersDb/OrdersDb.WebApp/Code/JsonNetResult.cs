using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OrdersDb.WebApp.Code
{
    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public JsonNetResult()
        {
            JsonSerializerSettings = new JsonSerializerSettings
                                     {
                                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                         DefaultValueHandling = DefaultValueHandling.Ignore,
                                         NullValueHandling = NullValueHandling.Ignore
                                     };
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            var response = context.HttpContext.Response;
            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data == null)
                return;
            JsonSerializerSettings.Converters.Add(new IsoDateTimeConverter
                                    {
                                        Culture = new CultureInfo("ru-RU"),
                                        DateTimeFormat = "dd.MM.yyyy HH:mm:ss"
                                    });

            var serializedObject = JsonConvert.SerializeObject(Data, Formatting.Indented, JsonSerializerSettings);
            response.Write(serializedObject);
        }
    }
}