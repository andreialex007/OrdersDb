using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using OrdersDb.Domain.Exceptions;

namespace OrdersDb.WebApp.Controllers._Common
{
    public class ExceptionControllerActionInvoker : ControllerActionInvoker
    {
        //#if !DEBUG 
        protected override ActionResult InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            try
            {
                return base.InvokeActionMethod(controllerContext, actionDescriptor, parameters);
            }
            catch (ValidationException exception)
            {
                return new JsonResult
                       {
                           Data = new
                                  {
                                      exception.ValidationSummary,
                                      exception.Errors,
                                      HasErrors = true
                                  },
                           JsonRequestBehavior = JsonRequestBehavior.AllowGet
                       };
            }
        }
        //#endif
    }
}