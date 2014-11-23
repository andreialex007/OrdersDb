﻿using System.Web.Mvc;
using OrdersDb.Domain.Services.Geography.City;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItem(Icon = "fa-institution", Name = "Города")]
    public class CitiesController : NamedEntityControllerBase<ICityService, City, CitySearchParameters, CityDto>
    {
        public CitiesController(ICityService service)
            : base(service)
        {
        }

        public ActionResult GetCitiesInRegion(int regionId)
        {
            return Json(_service.GetCitiesInRegion(regionId));
        }
    }
}