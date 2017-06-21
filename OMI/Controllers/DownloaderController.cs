using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMI.Controllers
{
    public class DownloaderController : Controller
    {
        public ActionResult Upload(HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);//obtenemos el nombre del archivo a cargar
            file.SaveAs(Server.MapPath(@"~\Content\" + fileName));//guardamos el archivo en la ruta física que corresponde a la ruta virtual del archivo
            return RedirectToAction("Index","Home");//volvemos a la página principal
        }
        public Download DownloadFile()
        {

            return new Download
            {

                FileName = "FI_DI_01.pdf",
                Path = @"~/Download/Files/FI_DI_01.pdf"

            };
        }
        public Download DownloadFile(string nombre,string tipo)
        {
            string completo = nombre.Trim() + "." + tipo.Trim();
            return new Download
            {

                FileName = completo,
                Path = @"~/Download/Files/"+completo

            };
        }
    }


    public class Download : ActionResult
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public override void ExecuteResult(ControllerContext context)
        {
            {
                context.HttpContext.Response.Buffer = true;
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                context.HttpContext.Response.WriteFile(context.HttpContext.Server.MapPath(Path));
            }
        }
    }
}