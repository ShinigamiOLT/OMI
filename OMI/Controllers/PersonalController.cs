using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using OMI.Models;

namespace OMI.Controllers
{
    public class PersonalController : Controller
    {
        private static object MapToGridModel(TbPedidoPersonal o)
        {
            return
                new
                {
                    o.Id,
                  Especialidad=  o.TbEspecialidad.Nombre,
                
                   Profesion= o.TbProfesion.Nombre,
                    Categoria = o.TbCategoriaRH.Nombre,
                    o.Cantidad,
                    IdSupervisor = o.TbUsuario.Nombre,
                    Estatus = o.TbStatusAutorizacion.Nombre,
                    Descripcion= o.Descripcion
              
              /*
 Date = o.Date.ToShortDateString(),
                       ChefName = o.Chef.FirstName + " " + o.Chef.LastName,
                     Meals = string.Join(", ", o.Meals.Select(m => m.Name))*/
                };
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            int id = 1;
            if (Session["IdSolicitud"] != null)
                id = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id, 3);
            search = (search ?? "").ToLower();
            var items = sol.contexto.TbPedidoPersonal.Where(o => o.IdSolicitud == sol.TbSol.IdSolicitud).Where(o => o.Dato != 2).AsQueryable();//.OrderBy(m=>m.Id);

            return Json(new GridModelBuilder<TbPedidoPersonal>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem = () => sol.contexto.TbPedidoPersonal.Find(Convert.ToInt32(g.Key), sol.TbSol.IdSolicitud), // called by the grid.api.update ( edit popupform success js func )
                Map = MapToGridModel
            }.Build());
        }

        // GET: Personal
        public ActionResult Index(int? ID)
        {
            cSolicitud sol = new cSolicitud(ID ?? 0,3);
           
            Session["IdSolicitud"] = sol.TbSol.IdSolicitud;
            return View(sol);
        }

        public ActionResult Create()
        {
           PedidoPInPut personal = new PedidoPInPut();
            return PartialView(personal);
        }
        [HttpPost]
        public ActionResult Create(PedidoPInPut input)
        { if (! ModelState.IsValid)
            return PartialView();
            int id = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id, 2);

            TbPedidoPersonal dinner = new TbPedidoPersonal()
            {
                Id = sol.GeneraIdPedidoP(),
                IdSolicitud =  sol.TbSol.IdSolicitud,
                IdTipoEspecialidad = input.Especialidad,
                IdProfesion = input.Profesion,
                IdCategoriaRH = input.Categoria,
                Cantidad =input.Cantidad,
                Descripcion = input.Descripcion,
                Estatus = 1,
                Supervisor = 1,
                TbUsuario =  sol.contexto.TbUsuario.Find(1),
                Dato = 0,
                TbCategoriaRH = sol.contexto.TbCategoriaRH.Find(input.Categoria),
                TbEspecialidad = sol.contexto.TbEspecialidad.Find(input.Especialidad),
                TbProfesion = sol.contexto.TbProfesion.Find(input.Profesion),

                TbStatusAutorizacion = sol.contexto.TbStatusAutorizacion.Find(1),
                TbSolicitud = sol.TbSol


            };

            sol.contexto.TbPedidoPersonal.Add(dinner);
            sol.contexto.SaveChanges();

            return Json(MapToGridModel(dinner));
        }

        public ActionResult Edit(int id)
        {
            int id_ = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id_, 3);
            if (!sol.Valido)
                return RedirectToAction("Index");
            var dinner = sol.contexto.TbPedidoPersonal.Find(id, sol.TbSol.IdSolicitud);

            var input = new PedidoPInPut()
            {
                Id = dinner.Id,
                IdSolicitud = dinner.IdSolicitud,
                Especialidad = dinner.IdTipoEspecialidad??1,
                Profesion   = dinner.IdProfesion??1,
                Cantidad = dinner.Cantidad??1,
                Categoria = dinner.IdCategoriaRH??1,
                Descripcion = dinner.Descripcion
                
               
            };

            return PartialView("Create", input);
        }
        [HttpPost]
        public ActionResult Edit(PedidoPInPut input)
        {
            if (!ModelState.IsValid)
                return PartialView("Create", input);
            int id = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id, 3);
            if (!sol.Valido)
                return RedirectToAction("Index");
            sol.UpdatePedidoP(input);


            // returning the key to call grid.api.update
            return Json(new { Id = input.Id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            int id_ = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id_, 3);
            if (!sol.Valido)
                return RedirectToAction("Index");
            var dinner = sol.GetPedidoP(id, sol.TbSol.IdSolicitud);

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                GridId = gridId,
                Message = string.Format("¿Estas Seguro que quieres Elimina el pedido de <b>{0}</b> ?", dinner.TbProfesion.Nombre)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            int id = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id, 3);
            sol.DelPedidoP(input.Id);
            return Json(new { Id = input.Id });
        }


        public ActionResult Guadar()
        {
            int id = (int)Session["IdSolicitud"];

            if (ModelState.IsValid)
            {
                cSolicitud sol = new cSolicitud(id, 3);
                sol.GuardaPedidos();

            }

            return RedirectToAction("Index", "Personal", new
            {
                id
            });
        }


        public ActionResult Cancelar()
        {
            int id = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id, 1);
            sol.EliminaPedidos(0);
            sol.DesMarcaEliminado(2);
            return RedirectToAction("Index", "Personal", new { id });
        }
        public ActionResult Enviar()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Details(int id)
        {
            return RedirectToAction("Index", "Personal", new { id });
        }
        public ActionResult DeleteM(int id)
        {
            return RedirectToAction("AllSolicitud", "Solicitud",new {id});
        }
    }
}