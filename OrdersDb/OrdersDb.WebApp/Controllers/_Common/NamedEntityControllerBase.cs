using System.Web.Mvc;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.WebApp.Controllers._Common
{
    public class NamedEntityControllerBase<IServiceType, TEntity, TSearchParameters, TDto> : ControllerBase<IServiceType, TEntity, TSearchParameters, TDto>
        where IServiceType : INamedServiceBase<TEntity, TSearchParameters, TDto>
        where TSearchParameters : SearchParameters
        where TEntity : EntityBase, INamedEntity, new()
        where TDto : DtoBase, new()
    {
        public NamedEntityControllerBase(IServiceType service)
            : base(service)
        {
        }

        public ActionResult GetAllNameValues()
        {
            return Json(_service.GetAllNameValues());
        }
    }
}