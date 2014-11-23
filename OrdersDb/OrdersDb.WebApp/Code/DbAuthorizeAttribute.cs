using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.WebApp.Code.Extensions;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Code
{
    public class DbAuthorizeAttribute : AuthorizeAttribute
    {
        private AccessType AccessType;
        private ActionDescriptor ActionDescriptor;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var permissions = httpContext.User.GetPermissions();

            if (AccessType == AccessType.Read)
            {
                if (permissions.Reads.Contains(ActionDescriptor.ControllerDescriptor.ControllerName))
                    return true;
            }

            if (AccessType == AccessType.Add)
            {
                if (permissions.Adds.Contains(ActionDescriptor.ControllerDescriptor.ControllerName))
                    return true;
            }

            if (AccessType == AccessType.Delete)
            {
                if (permissions.Deletes.Contains(ActionDescriptor.ControllerDescriptor.ControllerName))
                    return true;
            }

            if (AccessType == AccessType.Update)
            {
                if (permissions.Updates.Contains(ActionDescriptor.ControllerDescriptor.ControllerName))
                    return true;
            }

            return base.AuthorizeCore(httpContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var actionAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AccessTypeAttribute), true);
            var controllerAttributes = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AccessTypeAttribute), true);
            var accessTypeAttribute = (AccessTypeAttribute)(actionAttributes.FirstOrDefault() ?? controllerAttributes.First());
            ActionDescriptor = filterContext.ActionDescriptor;
            AccessType = accessTypeAttribute.AccessType;
            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = HttpContext.Current.IsJsonRequest()
                    ? (ActionResult)new JsonResult { Data = new { result = "Access denied." } }
                    : new RedirectResult("~/login");
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}