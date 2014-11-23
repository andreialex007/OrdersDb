using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.WebApp.Code;
using WebGrease.Css.Extensions;

namespace OrdersDb.WebApp.Controllers._Common
{
    public class ControllerBase<IServiceType, TEntity, TSearchParameters, TDto> : ControllerBase
        where IServiceType : IServiceBase<TEntity, TSearchParameters, TDto>
        where TEntity : EntityBase, new()
        where TSearchParameters : SearchParameters
        where TDto : DtoBase
    {
        protected readonly IServiceType _service;

        public ControllerBase(IServiceType service)
        {
            _service = service;

        }

        #region Базовые crud операции

        [AccessType(AccessType.Add)]
        [HttpPost]
        public virtual ActionResult Add(TEntity region)
        {
            _service.Add(region);
            return SuccessJsonResult();
        }

        [AccessType(AccessType.Update)]
        [HttpPost]
        public virtual ActionResult Update(TEntity region)
        {
            _service.Update(region);
            return SuccessJsonResult();
        }

        [AccessType(AccessType.Delete)]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return SuccessJsonResult();
        }

        [AccessType(AccessType.Delete)]
        public ActionResult DeleteMany(int[] ids)
        {
            ids.ForEach(x => _service.Delete(x));
            return SuccessJsonResult();
        }

        [HttpPost]
        public virtual ActionResult GetById(int id)
        {
            return Json(_service.GetById(id));
        }

        public virtual ActionResult Search(TSearchParameters parameters, bool append = false)
        {
            return Json(new
            {
                List = _service.Search(parameters),
                Total = !append ? _service.Total(parameters) : 0,
            });
        }

        #endregion
    }

    [AccessType(AccessType.Read)]
    public class ControllerBase<IServiceType, TEntity, TDto> : ControllerBase<IServiceType, TEntity, SearchParameters, TDto>
        where IServiceType : IServiceBase<TEntity, SearchParameters, TDto>
        where TEntity : EntityBase, new()
        where TDto : DtoBase, new()
    {
        public ControllerBase(IServiceType service)
            : base(service)
        {
        }
    }

    [DbAuthorize]
    [AccessType(AccessType.Read)]
    public class ControllerBase : Controller
    {
        public ControllerBase()
        {
            ActionInvoker = new ExceptionControllerActionInvoker();
        }

        protected override JsonResult Json(object data, string contentType,
            Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
                   {
                       Data = data,
                       ContentType = contentType,
                       ContentEncoding = contentEncoding,
                       JsonRequestBehavior = behavior
                   };
        }


        protected JsonResult SuccessJsonResult()
        {
            return Json(new { Result = "ok" });
        }

    }
}