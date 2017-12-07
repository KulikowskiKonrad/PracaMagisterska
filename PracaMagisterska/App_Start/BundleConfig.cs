using System.Web;
using System.Web.Optimization;

namespace PracaMagisterska
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                           "~/Scripts/jquery.validate*",
                           "~/Scripts/bootstrap.js",
                           "~/Scripts/autosize.js",
                           "~/Scripts/sweetalert2.js",
                           "~/Scripts/moment-with-locales.js",
                           "~/Scripts/bootstrap-datetimepicker.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/sweetalert2.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/site.css"));
        }
    }
}
