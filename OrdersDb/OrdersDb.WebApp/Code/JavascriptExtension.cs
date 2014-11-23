using System;
using System.IO;
using System.Web.Caching;
using System.Web.Mvc;

namespace OrdersDb.WebApp.Code
{
    /// <summary>
    /// http://stackoverflow.com/a/8316991/1346297
    /// </summary>
    public static class JavascriptExtension
    {
        public static MvcHtmlString IncludeVersionedJs(this HtmlHelper helper, string filename)
        {
            var version = GetVersion(helper, filename);
            return MvcHtmlString.Create(string.Format("<script type='text/javascript' src='{0}{1}'></script>", filename, version));
        }

        private static string GetVersion(this HtmlHelper helper, string filename)
        {
            var context = helper.ViewContext.RequestContext.HttpContext;

            if (context.Cache[filename] == null)
            {
                var physicalPath = context.Server.MapPath(filename);
                var version = string.Format("?v={0}", new FileInfo(physicalPath).LastWriteTime
                    .ToString("yyyyMMddhhmmss"));
                context.Cache.Add(physicalPath, version, null,
                    DateTime.Now.AddMinutes(1), TimeSpan.Zero,
                    CacheItemPriority.Normal, null);
                context.Cache[filename] = version;
                return version;
            }
            return context.Cache[filename] as string;
        }
    }
}