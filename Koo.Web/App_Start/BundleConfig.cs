using System.Web;
using System.Web.Optimization;

namespace Koo.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/metro").Include(
                        "~/js/metro.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                //"~/Scripts/jquery-{version}.min.js",
                        //"~/js/jquery/jquery.min.js",
                        "~/js/jquery/jquery.mousewheel.js",
                        "~/js/jquery/jquery.widget.min.js",
                //--------------------------//
                        "~/js/prettify/prettify.js",
                        "~/Scripts/jquery-1.10.2.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery.widget*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //    //"~/Scripts/bootstrap.js",
            //    //"~/Scripts/respond.js",
            //    //"~/js/metro/metro-*",
            //          "~/js/metro.min.js"
            //          ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/css/metro-bootstrap.css",
                      "~/css/metro-bootstrap-responsive.css",
                //"~/css/metro-bootstrap.min.css",
                //"~/css/metro-bootstrap-reponsive.min.css",
                      "~/css/iconFont.min.css",
                      "~/js/prettify/prettify.css"
                      ));

            // 将 EnableOptimizations 设为 false 以进行调试。有关详细信息，
            // 请访问 http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
            //BundleTable.EnableOptimizations = true;
        }
    }
}
