using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using OMI.Models;

namespace OMI.Controllers
{
    public class SolicitudController : Controller
    {cSolicitud sol = new cSolicitud();
        private static object MapToGridModel(TbPedidoM o)
        {
            return
                new
                {
                    o.Id,
                    o.Descripcion,
                    o.Cantidad,
                    IdSupervisor= o.Supervisores.TbUsuario.Nombre,
                    Estatus =  o.TbStatusAutorizacion.Nombre,
                  Unidad=  o.TbUnidad.Nombre,
                  Categoria=o.TbCategoria.Nombre
                 
                   /* Date = o.Date.ToShortDateString(),
                    ChefName = o.Chef.FirstName + " " + o.Chef.LastName,
                    Meals = string.Join(", ", o.Meals.Select(m => m.Name))*/
                };
        }
   

       

        // GET: Solicitud
        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult NuevaSolicitud()
        {
            
            return View(sol);
        }

       [HttpPost]
       public ActionResult NuevaSolicitud(cSolicitud sol_)
       {
           if (ModelState.IsValid)
           {
               sol_.GuardaPedidos();
              
           }
           return RedirectToAction("NuevaSolicitud", "Solicitud");
       }
     
        public ActionResult Cancelar()
        {
           sol. EliminaPedidos(0);
            sol.DesMarcaEliminado(2);
            return RedirectToAction("NuevaSolicitud", "Solicitud");
        }
        public ActionResult Enviar()
        {
            return RedirectToAction("Index","Home");
        }


        public ActionResult GridGetItems(GridParams g, string search)
        {
          
            search = (search ?? "").ToLower();
            var items = sol.contexto.TbPedidoM.Where(o => o.IdSolicitud == sol.TbSol.IdSolicitud).Where(o => o.Dato !=2).AsQueryable();//.OrderBy(m=>m.Id);

            return Json(new GridModelBuilder<TbPedidoM>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem = () => sol.Get<TbPedidoM>(Convert.ToInt32(g.Key),sol.TbSol.IdSolicitud), // called by the grid.api.update ( edit popupform success js func )
                Map = MapToGridModel
            }.Build());
        }

        public ActionResult GridGetPedidoMaterial(GridParams g, string search)
        {
           OPEntities contexto = new OPEntities();
          var items =  contexto.PedidoMXSolicitud(2).ToList().AsQueryable();


        
            return Json(new GridModelBuilder<PedidoMXSolicitud_Result>(items, g)
            {
                Key = "Id",
                Map = o => new
                {
                  Folio= o.Formato+ o.Folio,
                 Fecha= o.Fecha.ToShortDateString(),
                  Categoria=  o.Nombre,
                 Descripcion=  o.Descripcion,
                Unidad=   o.Unidad,
               o.Cantidad,
                    o.Autoriza,
                 Estatus=   o.Estatus,
                 Usuario=   o.Usuario
                },
            }.Build());
        }
        

        public ActionResult Create()
        {
            TbPedidoM dinner = new TbPedidoM();

            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(PedidoInPut input)
        {
            if (!ModelState.IsValid) return PartialView(input);
           

            TbPedidoM dinner = (new TbPedidoM()
            {
              
                IdSolicitud = sol.TbSol.IdSolicitud,
                IdCategoria = input.Categoria,
                Descripcion = input.Descripcion,
                Estatus = 1,
                IdSupervisor = 1,
               
                Surtido = 1,
                Cantidad = input.Cantidad,
                TbUnidad =  sol.contexto.TbUnidad.Find(input.IdUnidad),
                TbCategoria =  sol.GetCategoria (input.Categoria),
                 Supervisores =  sol.contexto.Supervisores.Find(1),
                 TbStatusAutorizacion = sol.contexto.TbStatusAutorizacion.Find(1),
                 Dato = 0,
                
                 
                /*
                Chef = Db.Get<Chef>(input.Chef),
                Meals = Db.Meals.Where(o => input.Meals.Contains(o.Id)),
              */
            });
         sol.AgregaPedido(dinner);
           //  sol.ListaPedido.Add(dinner);
            return Json(MapToGridModel(dinner)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(int id)
        {
            var dinner = sol.GetPedidoM(id,sol.TbSol.IdSolicitud);

            var input = new PedidoInPut()
            {
                Id = dinner.Id,
                IdUnidad = dinner.IdUnidad,
                Descripcion = dinner.Descripcion,
                IdSolicitud = dinner.IdSolicitud,
                Cantidad = dinner.Cantidad,
                Categoria = dinner.IdCategoria
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(PedidoInPut input)
        {
            if (!ModelState.IsValid)
                return PartialView("Create", input);
            sol.UpdatePedido(input);

           
            // returning the key to call grid.api.update
            return Json(new { Id = input.Id });
        }
        public ActionResult Delete(int id, string gridId)
        {
            var dinner =sol.GetPedidoM(id,sol.TbSol.IdSolicitud);

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                GridId = gridId,
                Message = string.Format("¿Estas Seguro que quieres Elimina el pedido de <b>{0}</b> ?", dinner.Descripcion)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            sol.DelPedido2(input.Id);
            return Json(new { Id = input.Id });
        }


        public ActionResult Lista()
        {
            
            return View();
        }

    }
}