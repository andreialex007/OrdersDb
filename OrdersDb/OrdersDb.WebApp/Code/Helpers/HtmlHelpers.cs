using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace OrdersDb.WebApp.Code.Helpers
{
    public static class HtmlHelpers
    {
        public static bool IsDebug(this HtmlHelper htmlHelper)
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        public static MvcHtmlString PartialIfExists(this HtmlHelper htmlHelper, string partialViewName)
        {
            var controllerContext = htmlHelper.ViewContext.Controller.ControllerContext;
            var result = ViewEngines.Engines.FindView(controllerContext, partialViewName, null);
            if (result.View == null)
                return new MvcHtmlString(string.Empty);
            return htmlHelper.Partial(partialViewName, (object)null, htmlHelper.ViewData);
        }
    }
}