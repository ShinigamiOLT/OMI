using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using OMI.Models;

namespace OMI.Controllers
{
    public class InfraestructuraController : Controller
    {
        private static object MapToGridModel(TbPedidoM o)
        {
            return
                new
                {
                    o.Id,
                    o.Descripcion,
                    o.Cantidad,
                    IdSupervisor = o.Supervisores.TbUsuario.Nombre,
                    Estatus = o.TbStatusAutorizacion.Nombre,
                    Unidad = o.TbUnidad.Nombre,
                    Categoria = o.TbCategoria.Nombre

                    /* Date = o.Date.ToShortDateString(),
                     ChefName = o.Chef.FirstName + " " + o.Chef.LastName,
                     Meals = string.Join(", ", o.Meals.Select(m => m.Name))*/
                };
        }



        public ActionResult GridGetItems(GridParams g, string search)
        {
            int id = 1;
            if (Session["IdSolicitud"] != null)
                id = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id, 1);
            if (!sol.Valido)
                return RedirectToAction("Index");
            search = (search ?? "").ToLower();
            var items = sol.contexto.TbPedidoM.Where(o => o.IdSolicitud == sol.TbSol.IdSolicitud).Where(o => o.Dato != 2).AsQueryable();//.OrderBy(m=>m.Id);

            return Json(new GridModelBuilder<TbPedidoM>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem = () => sol.Get<TbPedidoM>(Convert.ToInt32(g.Key), sol.TbSol.IdSolicitud), // called by the grid.api.update ( edit popupform success js func )
                Map = MapToGridModel
            }.Build());
        }


        // GET: Infraestructura
        public ActionResult Requermientos()
        {
            OPEntities context = new OPEntities();

            return View(context.TbSolicitud.Where(o => o.IdFormato == 1 && o.EnviadoInfra ==1).ToList());
        }
        public ActionResult Details(int? id)
        {
           


            cSolicitud sol = new cSolicitud(id ?? 0, 1);
            if (!sol.Valido)
                return RedirectToAction("Index");
            Session["IdSolicitud"] = sol.TbSol.IdSolicitud;
            ViewBag.Visible = sol.TbSol.EnviadoCom;
            return View(sol);
        }

        public ActionResult Edit(int id)
        {

            AutorizaInput nuevo = new AutorizaInput();

            return PartialView(nuevo);
        }

        [HttpPost]
        public ActionResult Edit(AutorizaInput input)
        {
            if (!ModelState.IsValid)
                return PartialView("Create", input);
            int id = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id, 1);
            if (!sol.Valido)
                return RedirectToAction("Index");
            sol.UpdatePedido(input);


            // returning the key to call grid.api.update
            return Json(new { Id = input.id });
        }
        public ActionResult Enviar()
        {
            //se supone que aqui enviaremos los documentos.
            int id = (int)Session["IdSolicitud"];

            cSolicitud sol = new cSolicitud(id, 1);
            sol.EnviarPedidoCom();


            return RedirectToAction("Requermientos", "Infraestructura", new
            {
                id
            });
        }
    }
}