using System.Web;
using System.Web.Optimization;

namespace Gharbhetibaa
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.js"));

            // Jquery validator & unobstrusive ajax  
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.unobtrusive-ajax.js", "~/Scripts/jquery.unobtrusive-ajax.min.js", "~/Scripts/jquery.validate*", "~/Scripts/jquery.validate.unobtrusive.js", "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/sweetalert").Include(
                      "~/Scripts/sweetalert.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Site_Custom/form").Include(
                      "~/Scripts/Site_Custom/form.js"));

            bundles.Add(new ScriptBundle("~/bundles/Site_Custom/formpages").Include(
                      "~/Scripts/Site_Custom/formpages.js"));

            bundles.Add(new ScriptBundle("~/bundles/Site_Custom/accordion").Include(
                      "~/Scripts/Site_Custom/accordion.js"));

            //Bootstrap Graph
            bundles.Add(new ScriptBundle("~/bundles/Site_Custom/bootstrapgraph").Include(
                      "~/Scripts/Site_Custom/mdb.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/fontawesome-all.css",
                      "~/Content/Site_Custom/style.css"));
        }
    }
}
