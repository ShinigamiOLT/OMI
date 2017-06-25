using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omu.AwesomeMvc;

namespace OMI.Controllers
{
    public class DataController : Controller
    {
        public OPEntities contexto;

        public DataController()
        {
            contexto= new OPEntities();
        }
        public ActionResult GetUnidad()
        {
            List<KeyContent> items = new List<KeyContent>();
            foreach (var unidad in contexto.TbUnidad)
            {
                items.Add(new KeyContent(unidad.Id, unidad.Nombre));
            }

            return Json(items);
        }
        public ActionResult GetProfesion()
        {
            List<KeyContent> items = new List<KeyContent>();
            foreach (var unidad in contexto.TbProfesion)
            {
                items.Add(new KeyContent(unidad.Id, unidad.Nombre));
            }

            return Json(items);
        }

        public ActionResult GetEspecialidad()
        {
            List<KeyContent> items = new List<KeyContent>();
            foreach (var unidad in contexto.TbEspecialidad)
            {
                items.Add(new KeyContent(unidad.Id, unidad.Nombre));
            }

            return Json(items);
        }
        public ActionResult GetCategoriaRH()
        {
            List<KeyContent> items = new List<KeyContent>();
            foreach (var unidad in contexto.TbCategoriaRH)
            {
                items.Add(new KeyContent(unidad.Id, unidad.Nombre));
            }

            return Json(items);
        }
        public ActionResult GetCategoria()
        {
            List<KeyContent> items = new List<KeyContent>();
            foreach (var unidad in contexto.TbCategoria)
            {
                items.Add(new KeyContent(unidad.Id, unidad.Nombre));
            }

            return Json(items);
        }

        public ActionResult GetAutorizacion()
        {
            List<KeyContent> items = new List<KeyContent>();
            foreach (var unidad in contexto.TbStatusAutorizacion)
            {
                items.Add(new KeyContent(unidad.Id, unidad.Nombre));
            }

            return Json(items);
        }
    }
}