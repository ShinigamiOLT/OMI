using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using OMI.Models;

namespace OMI.Controllers
{
    public class SolicitudController : Controller
    {
        private static object MapToGridModel(TbPedidoM o)
        {
            return
                new
                {
                    o.Id,
                    o.Descripcion,
                    o.Cantidad,
                    o.Estatus,
                    o.IdSupervisor,
                  Unidad=  o.TbUnidad.Nombre,
              
                  Categoria=o.TbCategoria.Nombre
                   /* Date = o.Date.ToShortDateString(),
                    ChefName = o.Chef.FirstName + " " + o.Chef.LastName,
                    Meals = string.Join(", ", o.Meals.Select(m => m.Name))*/
                };
        }
   cSolicitud sol ;

        public SolicitudController()
        {
            sol = new cSolicitud();
        }

        // GET: Solicitud
        public ActionResult Index()
        {
            return View();
        }

        public string Datos(int id)
        {
         

            return sol.Datos(id);
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
               sol.GuardaPedidos();
               sol.contexto.SaveChanges();
           }
           return RedirectToAction("Index");
       }
        

        public ActionResult GridGetItems(GridParams g, string search)
        {
          
            search = (search ?? "").ToLower();
            var items = sol.contexto.TbPedidoM.Where(o => o.IdSolicitud == 1).AsQueryable().OrderBy(m=>m.Id);

            return Json(new GridModelBuilder<TbPedidoM>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem = () => sol.Get<TbPedidoM>(Convert.ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                Map = MapToGridModel
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
              
                IdSolicitud = sol.IdSolicitud,
                IdCategoria = input.Categoria,
                Descripcion = input.Descripcion,
                Estatus = 1,
                IdSupervisor = 1,
                Surtido = 1,
                Cantidad = input.Cantidad,
                TbUnidad =  sol.contexto.TbUnidad.Find(input.IdUnidad),
                TbCategoria =  sol.GetCategoria (input.Categoria)
                /*
                Chef = Db.Get<Chef>(input.Chef),
                Meals = Db.Meals.Where(o => input.Meals.Contains(o.Id)),
              */
            });
            sol.contexto.TbPedidoM.Add(dinner);
           
            return Json(MapToGridModel(dinner)); // returning grid model, used in grid.api.renderRow
        }
    }
}