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
using OMI.Models.Utils;
using  System.Data.Entity;
using OIM_DAL;

namespace OMI.Controllers
{
    public class SolicitudController : Controller
    {
      
      

       

        // GET: Solicitud
        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult NuevaSolicitud(int? ID)
        {

            cSolicitud sol = new cSolicitud(ID ?? 0,1);
            if(!sol.Valido)
            return  RedirectToAction("Index");
            Session["IdSolicitud"] = sol.TbSol.IdSolicitud;
            ViewBag.Visible = sol.TbSol.EnviadoInfra;
            return View(sol);
        }

       [HttpPost]
       public ActionResult NuevaSolicitud(cSolicitud sol_)
       {
           if (ModelState.IsValid)
           {
               sol_.GuardaPedidos();
              
           }
           int id = (int)Session["IdSolicitud"];
            return RedirectToAction("NuevaSolicitud", "Solicitud", new { id  });
        }
       public ActionResult Guadar()
        {
            int id = (int)Session["IdSolicitud"];
       
            if (ModelState.IsValid)
            {
               cSolicitud sol = new cSolicitud(id,1);
                sol.GuardaPedidos();
              
            }
          
            return RedirectToAction("NuevaSolicitud", "Solicitud", new { id
            });
        }
     
     
        public ActionResult Cancelar()
        {
            int id = (int) Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id,1);
            sol. EliminaPedidos(0);
            sol.DesMarcaEliminado(2);
            return RedirectToAction("NuevaSolicitud", "Solicitud", new {id});
        }
        public ActionResult Enviar()
        {
            //se supone que aqui enviaremos los documentos.
            int id = (int)Session["IdSolicitud"];

            cSolicitud sol = new cSolicitud(id, 1);
                sol.EnviarPedido();


            return RedirectToAction("NuevaSolicitud", "Solicitud", new
            {
                id
            });
        }


        public ActionResult GridGetItems(GridParams g, string search)
        {
            int id = 1;
          if (Session["IdSolicitud"]!=null)
         id = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id,1);
            if (!sol.Valido)
                return RedirectToAction("Index");
            search = (search ?? "").ToLower();
            var items = sol.contexto.TbPedidoM.Where(o => o.IdSolicitud == sol.TbSol.IdSolicitud).Where(o => o.Dato !=2).AsQueryable();//.OrderBy(m=>m.Id);

            return Json(new GridModelBuilder<TbPedidoM>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem = () => sol.Get<TbPedidoM>(Convert.ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                Map = MaptoGridModel.MapToGridModel
            }.Build());
        }

        public ActionResult GridGetPedidoMaterial(GridParams g, string search)
        {
           OIMEntity contexto = new OIMEntity();
          var items =  contexto.Sp_AllPedido().ToList().AsQueryable();
           

        
            return Json(new GridModelBuilder<Sp_AllPedido_Result>(items, g)
            {
                Key = "Id",
                Map = o => new
                {
                    Id=o.Id,
                  Folio= o.Formato+ o.Folio,
                 Fecha= o.Fecha.ToShortDateString(),
                  Categoria=  o.Categoria,
                 Descripcion=  o.Descripcion,
                Unidad=   o.Unidad,
               o.Cantidad,
                    o.Autoriza,
                 Estatus=   o.MiEstatus,
                 Usuario=   o.Usuario
                },
            }.Build());
        }
        

        public ActionResult Create()
        {
           
            PedidoInPut nuevo = new PedidoInPut();
            
            return PartialView(nuevo);
        }

        [HttpPost]
        public ActionResult Create(PedidoInPut input)
        {
            if (!ModelState.IsValid) return PartialView(input);
            int id = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id,1);
            
            TbPedidoM dinner = new TbPedidoM()
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
                OrdenCompra = 4
                 
                /*
                Chef = Db.Get<Chef>(input.Chef),
                Meals = Db.Meals.Where(o => input.Meals.Contains(o.Id)),
              */
            };
         sol.AgregaPedido(dinner);
         
           //  sol.ListaPedido.Add(dinner);
            return Json(   MaptoGridModel.MapToGridModel(dinner)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(int id)
        {
            int id_ = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id_,1);
            if (!sol.Valido)
                return RedirectToAction("Index");


            var dinner = sol.GetPedidoM(id);

            var input = new PedidoInPut()
            {
                Id = dinner.Id,
                IdUnidad = dinner.IdUnidad,
                Descripcion = dinner.Descripcion,
                IdSolicitud = dinner.IdSolicitud.Value,
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
            int id = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id,1);
            if (!sol.Valido)
                return RedirectToAction("Index");
            sol.UpdatePedido(input);

           
            // returning the key to call grid.api.update
            return Json(new { Id = input.Id });
        }
        public ActionResult Delete(int id, string gridId)
        {
            int id_ = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id_,1);
            if (!sol.Valido)
                return RedirectToAction("Index");
            var dinner =sol.GetPedidoM(id);

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
            int id = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id,1);
            sol.DelPedidoP(input.Id);
            return Json(new { Id = input.Id });
        }


        public ActionResult Lista()
        {
            
            return View();
        }

        public ActionResult Concentrado()
        {
            OIMEntity contexto = new OIMEntity();
            var items = contexto.Sp_AllPedido().ToList();


            return View(items);
        }
        public ActionResult Concentrado1()
        {
            OIMEntity contexto = new OIMEntity();
            var items = contexto.TbSolicitud.Include(a => a.TbPedidoM).ToList();


            return View(items);
        }
        public ActionResult AllSolicitud(int id=1)
        {
           OIMEntity context = new OIMEntity(); 

            return View(context.TbSolicitud.Where(o=> o.IdFormato ==id).ToList());
        }

        public ActionResult Details(int id, int idfor)
        {
            if (idfor==3)
                return RedirectToAction("Index", "Personal", new { id });
            return RedirectToAction("NuevaSolicitud", "Solicitud", new {id});
        }
        public ActionResult DeleteM(int id, int idfor)
        {if(idfor==3)
                return RedirectToAction("AllSolicitud", "Solicitud", new { id });
            return RedirectToAction("AllSolicitud", "Solicitud", new { id });
        }
        public ActionResult DetailsP(int id)
        {
            return RedirectToAction("Index", "Personal", new { id });
        }
        public ActionResult DeleteP(int id)
        {
            return RedirectToAction("AllSolicitud", "Solicitud", new { id });
        }

        public ActionResult Autorizar()//string parent, int p1, string a, string b)
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Autorizar(AutorizaInput entrada)
        {
            return Json(entrada);
        }
        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {

           // int idsol = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id,3);
            sol.contexto.TbSolicitud.Find(id).EnviadoInfra = 1;
            sol.contexto.SaveChanges();
        
            return Json(id, JsonRequestBehavior.AllowGet);
        }



    }
}