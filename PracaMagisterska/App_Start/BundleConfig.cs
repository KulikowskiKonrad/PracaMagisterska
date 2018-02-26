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
                           "~/Scripts/bootstrap-datetimepicker.js",
                           "~/Scripts/highcharts.js",
                           "~/Scripts/highcharts-more.js",
                           "~/Scripts/solid-gauge.src.js",
                           "~/Scripts/no-data-to-display.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundlesNiezalogowany/js").Include(
                      "~/Scripts/Niezalogowany/jquery/jquery.min.js",
                      "~/Scripts/Niezalogowany/bootstrap/js/bootstrap.bundle.min.js",
                      "~/Scripts/Niezalogowany/jquery-easing/jquery.easing.min.js",
                      "~/Scripts/Niezalogowany/magnific-popup/jquery.magnific-popup.min.js",
                      "~/Scripts/Niezalogowany/jqBootstrapValidation.js",
                      "~/Scripts/Niezalogowany/contact_me.js",
                      "~/Scripts/Niezalogowany/freelancer.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/sweetalert2.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/highcharts.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/ContentNiezalogowany/css").Include(
                 "~/Scripts/Niezalogowany/bootstrap/css/bootstrap.min.css",
                 "~/Scripts/Niezalogowany/font-awesome/css/font-awesome.min.css",
                 "~/Scripts/Niezalogowany/magnific-popup/magnific-popup.css",
                 "~/Content/Niezalogowany/freelancer.min.css"
                ));
        }
    }
}
