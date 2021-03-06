﻿using System.Web;
using System.Web.Optimization;

namespace OMI
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862, "~/Scripts/jquery.event.drag.js"
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));
            //"~/Scripts/jquery-3.1.1.js"

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                "~/Content/css/micss_.css"));
            bundles.Add(new StyleBundle("~/Content/css2").Include(
              
                "~/Content/site.css"));

            // jquery datataables js files
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/DataTables/jquery.dataTables.min.js",
                "~/Scripts/DataTables/dataTables.bootstrap.js"));

            // jquery datatables css file
            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                "~/Content/DataTables/css/dataTables.bootstrap.css"));

            // jquery jspdf js files
            bundles.Add(new ScriptBundle("~/bundles/jspdf").Include(
                "~/Scripts/jspdf/jspdf.debug.js",
                "~/Scripts/jspdf/html2pdf.js"
                ));
        }
    }
}
