using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OIM_DAL;
using Omu.AwesomeMvc;

namespace OMI.Controllers
{
    public class InventarioController : Controller
    {
        // GET: Inventario
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EquipoComputo()
        {
            OIMEntity contexto = new OIMEntity();
         var lista=   contexto.TbInve_Equipo_Comp.ToList();

            return View(lista);
        }


        public ActionResult CreaEquipo()
        {
            var lista = new TbInve_Equipo_Comp()
            {
                Tipo = "",
                Nombre = "",
                Numero = "",
                Serie = "",
                Procesador = "",
                Observacion = "",
                Accesorio = ""
                

            };

            lista.FechaAlta = lista.FechaMantenimiento = DateTime.Now;
            lista.IdUsuario = 1;
           
            OIMEntity db = new OIMEntity();
            ViewBag.Usuarios = new SelectList(db.TbUsuario.ToList(), "Id", "Nombre");
            return View(lista);
        }
        [HttpPost]
        public ActionResult CreaEquipo(TbInve_Equipo_Comp nuevo )
        {
          OIMEntity contexto = new OIMEntity();
            contexto.TbInve_Equipo_Comp.Add(nuevo);
            contexto.SaveChanges();
            ViewBag.Usuarios = new SelectList(contexto.TbUsuario.ToList(), "Id", "Nombre");
            return View(nuevo);
        }
    }
}