using System.Web;
using System.Web.Optimization;

namespace Iris10ReportUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/ts").Include( //not using this bundle
            //          "~/js/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.min.css"));

            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                    "~/Content/kendo/2017.3.1018/kendo.common-bootstrap.min.css",
                    "~/Content/kendo/2017.3.1018/kendo.mobile.all.min.css",
                    "~/Content/kendo/2017.3.1018/kendo.dataviz.min.css",
                    "~/ReportViewer/styles/telerikReportViewer.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                    "~/Scripts/kendo/2017.3.1018/jquery.min.js",
                    "~/Scripts/kendo/2017.3.1018/jszip.min.js",
                    "~/Scripts/kendo/2017.3.1018/kendo.all.min.js",
                    "~/Scripts/kendo/2017.3.1018/kendo.aspnetmvc.min.js",
                    "~/Scripts/kendo/2017.3.1018/kendo.modernizr.custom.js",
                    "~/ReportViewer/js/telerikReportViewer-13.0.19.222.js"
                ));

            //This Should be ~/js/main.js for develop setting and ~/main.js for publish setting

            bundles.Add(new ScriptBundle("~/bundles/iris").Include(
                "~/js/main.js"
            ));
        }
    }
}
