using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
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
            var lista = contexto.TbInve_Equipo_Comp.ToList();

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
            ViewBag.Title = "Agregar Nuevo Equipo de Computo";
            return View(lista);
        }

        [HttpPost]
        public ActionResult CreaEquipo(TbInve_Equipo_Comp nuevo)
        {
            if (!ModelState.IsValid)
                return View(nuevo);
            OIMEntity contexto = new OIMEntity();

            int Siguiente = 1;
            if (contexto.TbInve_Equipo_Comp.Any())
            {
                Siguiente = contexto.TbInve_Equipo_Comp.Max(x => x.Id) + 1;
            }
            nuevo.Id = Siguiente;
            contexto.TbInve_Equipo_Comp.Add(nuevo);
            contexto.SaveChanges();
            ViewBag.Usuarios = new SelectList(contexto.TbUsuario.ToList(), "Id", "Nombre");
            ViewBag.Title = "Agregar Nuevo Equipo de Computo";
            return RedirectToAction("EquipoComputo");
        }

        public ActionResult EditEquipo(int Id)
        {
            OIMEntity contexto = new OIMEntity();
            var datos = contexto.TbInve_Equipo_Comp.Find(Id);
            ViewBag.Usuarios = new SelectList(contexto.TbUsuario.ToList().OrderBy(x=>x.Nombre), "Id", "Nombre");
            ViewBag.Estado = new SelectList(contexto.Tb_EstadoEquipo.ToList(), "Id", "Nombre");

            return View(datos);
        }

        [HttpPost]
        public ActionResult EditEquipo(TbInve_Equipo_Comp equipo)
        {
            OIMEntity contexto = new OIMEntity();
            contexto.Entry(equipo).State = EntityState.Modified;
            contexto.SaveChanges();
            ViewBag.Usuarios = new SelectList(contexto.TbUsuario.ToList(), "Id", "Nombre");
            ViewBag.Estado = new SelectList(contexto.Tb_EstadoEquipo.ToList(), "Id", "Nombre");
            return RedirectToAction("EquipoComputo");
        }

        public ActionResult EquipoVarios()
        {
            OIMEntity contexto = new OIMEntity();
            var lista = contexto.TbInve_Equipo_Varios.ToList();
            return View(lista);
        }


        public ActionResult CreateVarios()
        {
            OIMEntity contexto = new OIMEntity();
            TbInve_Equipo_Varios varios = new TbInve_Equipo_Varios();
            ViewBag.Estado = new SelectList(contexto.Tb_EstadoEquipo.ToList(), "Id", "Nombre");
            return View(varios);
        }
        [HttpPost]
        public ActionResult CreateVarios(TbInve_Equipo_Varios nuevo)
        {
            OIMEntity contexto = new OIMEntity();
            int Siguiente = 1;
            if (contexto.TbInve_Equipo_Comp.Any())
        {
            Siguiente = contexto.TbInve_Equipo_Comp.Max(x => x.Id) + 1;
        }
            nuevo.Id = Siguiente;
            contexto.TbInve_Equipo_Varios.Add(nuevo);
            contexto.SaveChanges();

            return RedirectToAction("EquipoVarios");

        }
        public ActionResult EditVarios(int Id)
        {
            OIMEntity contexto = new OIMEntity();
            var datos = contexto.TbInve_Equipo_Varios.Find(Id);
            ViewBag.Usuarios = new SelectList(contexto.TbUsuario.ToList().OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.Estado = new SelectList(contexto.Tb_EstadoEquipo.ToList(), "Id", "Nombre");

            return View(datos);
        }

        [HttpPost]
        public ActionResult EditVarios(TbInve_Equipo_Varios equipo)
        {
            OIMEntity contexto = new OIMEntity();
            contexto.Entry(equipo).State = EntityState.Modified;
            contexto.SaveChanges();
            ViewBag.Usuarios = new SelectList(contexto.TbUsuario.ToList(), "Id", "Nombre");
            ViewBag.Estado = new SelectList(contexto.Tb_EstadoEquipo.ToList(), "Id", "Nombre");
            return RedirectToAction("EquipoVarios");
        }


        public ActionResult Licencia()
        {
            OIMEntity contexto = new OIMEntity();
            var lista = contexto.TbInve_Licencias.ToList().OrderBy(x => x.Tipo).ToList();
            return View(lista);

        }

        public ActionResult EditLic(int Id)
        {
            OIMEntity contexto = new OIMEntity();
            var elemento = contexto.TbInve_Licencias.Find(Id);
            ViewBag.Equipo = new SelectList(contexto.TbInve_Equipo_Comp.ToList(), "Id", "Numero");
            ViewBag.Estado = new SelectList(contexto.Tb_EstadoLic.ToList(), "Id", "Nombre");
            return View(elemento);
        }

        [HttpPost]
        public ActionResult EditLic(TbInve_Licencias nuevo)
        {
            OIMEntity contexto = new OIMEntity();
            contexto.Entry(nuevo).State = EntityState.Modified;
            contexto.SaveChanges();
            ViewBag.Equipo = new SelectList(contexto.TbInve_Equipo_Comp.ToList(), "Id", "Numero");
            ViewBag.Estado = new SelectList(contexto.Tb_EstadoLic.ToList(), "Id", "Nombre");

            return RedirectToAction("Licencia");
        }

        public ActionResult CreateLic()
        {
            OIMEntity contexto = new OIMEntity();
            var elemento = new TbInve_Licencias();
            ViewBag.Equipo = new SelectList(contexto.TbInve_Equipo_Comp.ToList(), "Id", "Numero");
            ViewBag.Estado = new SelectList(contexto.Tb_EstadoLic.ToList(), "Id", "Nombre");
            return View(elemento);
        }

        [HttpPost]
        public ActionResult CreateLic(TbInve_Licencias nuevo)
        {
            OIMEntity contexto = new OIMEntity();
          
            ViewBag.Equipo = new SelectList(contexto.TbInve_Equipo_Comp.ToList(), "Id", "Numero");
            ViewBag.Estado = new SelectList(contexto.Tb_EstadoLic.ToList(), "Id", "Nombre");
            if (nuevo.Tipo == null)
                return View(nuevo);
            int Siguiente = 1;
            if (contexto.TbInve_Equipo_Comp.Any())
            {
                Siguiente = contexto.TbInve_Licencias.Max(x => x.Id) + 1;
            }
            nuevo.Id = Siguiente;
            contexto.TbInve_Licencias.Add(nuevo);
            contexto.SaveChanges();
            return RedirectToAction("Licencia");
        }

        public ActionResult Vehiculos()
        {
            OIMEntity contexto = new OIMEntity();
            var lista = contexto.TbInve_Vehiculos.ToList().OrderBy(x => x.Codigo).ToList();
            return View(lista);

        }

        public ActionResult CreateVehiculo()
        {
            OIMEntity contexto = new OIMEntity();
            TbInve_Vehiculos varios = new TbInve_Vehiculos();
          
            return View(varios);
        }
        [HttpPost]
        public ActionResult CreateVehiculo(TbInve_Vehiculos nuevo)
        {
            OIMEntity contexto = new OIMEntity();
            int Siguiente = 1;
            if (contexto.TbInve_Equipo_Comp.Any())
            {
                Siguiente = contexto.TbInve_Vehiculos.Max(x => x.Id) + 1;
            }
            nuevo.Id = Siguiente;
            contexto.TbInve_Vehiculos.Add(nuevo);
            contexto.SaveChanges();

            return RedirectToAction("Vehiculos");

        }


        public ActionResult EditVehiculo(int Id)
        {
            OIMEntity contexto = new OIMEntity();
            var elemento = contexto.TbInve_Vehiculos.Find(Id);
            return View(elemento);
        }

        [HttpPost]
        public ActionResult EditVehiculo(TbInve_Vehiculos nuevo)
        {
            OIMEntity contexto = new OIMEntity();
            contexto.Entry(nuevo).State = EntityState.Modified;
            contexto.SaveChanges();

            return RedirectToAction("Vehiculos");
        }
    }
}