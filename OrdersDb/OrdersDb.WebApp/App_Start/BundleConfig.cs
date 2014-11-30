using System.Collections.Generic;
using System.Web.Optimization;
using Antlr.Runtime.Misc;

namespace OrdersDb.WebApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //METRONIC STYLES  ******************
            //GLOBAL MANDATORY STYLES
            var globalMandatoryStyles = new[]
                          {
                              "~/Metronic/global/plugins/font-awesome/css/font-awesome.min.css",
                              "~/Metronic/global/plugins/simple-line-icons/simple-line-icons.min.css",
                              "~/Metronic/global/plugins/bootstrap/css/bootstrap.min.css",
                              "~/Metronic/global/plugins/uniform/css/uniform.default.css",
                              "~/Metronic/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css"
                          };

            //PAGE LEVEL PLUGIN STYLES
            var pageLevelPluginStyles = new[]
                          {
                              "~/Metronic/global/plugins/gritter/css/jquery.gritter.css",
                              "~/Metronic/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css",
                              "~/Metronic/global/plugins/fullcalendar/fullcalendar/fullcalendar.css",
                              "~/Metronic/global/plugins/jqvmap/jqvmap/jqvmap.css",
                              "~/Metronic/global/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.css",
                              "~/Metronic/global/plugins/select2/select2.css",
                              "~/Metronic/global/css/plugins.css",
                              "~/Metronic/admin/pages/css/tasks.css"
                          };

            //THEME STYLES
            var themeStyles = new[]
                          {
                              "~/Metronic/global/css/components.css",
                              "~/Metronic/global/css/plugins.css",
                              "~/Metronic/global/css/plugins.css",
                              "~/Metronic/admin/layout/css/layout.css",
                              "~/Metronic/admin/layout/css/themes/default.css",
                              "~/Metronic/admin/layout/css/custom.css"
                          };


            //APPLICATION STYLES ******************
            Func<Bundle, Bundle> applicationStyles = bundle => bundle.Include("~/Styles/common.css")
                .IncludeDirectory("~/Styles/controls", "*.css", true)
                .IncludeDirectory("~/Styles/pages", "*.css", true);

            //BUNDLES DEFINITION
            bundles.Add(new StyleBundle("~/css") { Orderer = new AsIsBundleOrderer() }
                .IncludeRewriteCssStyles(globalMandatoryStyles)
                .IncludeRewriteCssStyles(pageLevelPluginStyles)
                .IncludeRewriteCssStyles(themeStyles)
                .IncludeAction(applicationStyles)
                );

            bundles.Add(new StyleBundle("~/css-login") { Orderer = new AsIsBundleOrderer() }
                .IncludeRewriteCssStyles(globalMandatoryStyles)
                .IncludeRewriteCssStyles(pageLevelPluginStyles)
                .IncludeRewriteCssStyles(themeStyles)
                .IncludeRewriteCssStyles(new[]
                                         {
                                             "~/Metronic/global/plugins/select2/select2.css",
                                             "~/Metronic/admin/pages/css/login.css"
                                         })
                .IncludeAction(applicationStyles)
                );

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }

    public class AsIsBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }

    public static class BundlesExtensions
    {
        public static Bundle IncludeRewriteCss(this Bundle bundle, string path)
        {
            return bundle.Include(path, new CssRewriteUrlTransform());
        }

        public static Bundle IncludeRewriteCssStyles(this Bundle bundle, params string[] paths)
        {
            foreach (var path in paths)
                bundle.Include(path, new CssRewriteUrlTransform());

            return bundle;
        }

        public static Bundle IncludeAction(this Bundle bundle, Func<Bundle, Bundle> func, params IItemTransform[] transforms)
        {
            return func(bundle);
        }
    }

}
