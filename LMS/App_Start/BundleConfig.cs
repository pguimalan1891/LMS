using System.Web;
using System.Web.Optimization;

namespace LMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Scripts/jquery-{version}.js",
                        "~/Content/jquery/jquery-ui-1.12.1/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/bootstrap/js/bootstrap.min.js",
                      "~/Content/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                      "~/Content/bootstrap/css/bootstrap.min.css",
                      "~/Content/jquery/jquery-ui-1.12.1/jquery-ui.css",
                      "~/Content/metroBootstrap/dist/css/metro-bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/DataTableCSS").Include(
                    "~/Content/datatable/media/css/dataTables.bootstrap.min.css"                    
                    ));

            bundles.Add(new ScriptBundle("~/Content/DataTableJS").Include(
                    "~/Content/datatable/media/js/jquery.dataTables.min.js",
                    "~/Content/datatable/media/js/dataTables.bootstrap.min.js",
                    "~/Content/datatable/media/js/dataTables.responsive.min.js",
                    "~/Content/datatable/media/js/responsive.bootstrap.min.js"
                    ));
        }
    }
}
