using System.Web;
using System.Web.Mvc;
using OrdersDb.Domain.Services.Geography.Country;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Code.Extensions;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItem(Icon = "fa-flag", Name = "Страны")]
    public class CountriesController : NamedEntityControllerBase<ICountryService, Country, CountrySearchParameters, CountryDto>
    {
        public CountriesController(ICountryService service)
            : base(service)
        {
        }

        [HttpPost]
        public ActionResult UploadFlag(HttpPostedFileBase file, int? id)
        {
            _service.UploadFlag(file.ToByteArray(), id);
            return SuccessJsonResult();
        }

        [HttpGet]
        public ActionResult GetFlag(int? id)
        {
            var flagData = _service.GetFlag(id) ?? System.IO.File.ReadAllBytes(Server.MapPath("~/Images/default-flag.jpg"));
            return File(flagData, System.Net.Mime.MediaTypeNames.Image.Jpeg);
        }

    }
}