using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OIM_DAL;
using Omu.AwesomeMvc;
using OMI.Models;
using OMI.Models.Utils;
using  System.Data.Entity;

namespace OMI.Controllers
{
    public class AdmonController : Controller
    {
        // GET: Admon
        public ActionResult SolCotizacion()
        {
            OIMEntity context = new OIMEntity();

            return View(context.TbSolicitud.Where(o => o.IdFormato == 1 && o.EnviadoCom == 1).ToList());
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

        public ActionResult GridGetItems1(GridParams g, string search)
        {
            int id = 1;
            if (Session["IdSolicitud"] != null)
                id = (int) Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id, 1);
            if (!sol.Valido)
                return RedirectToAction("Index");
            search = (search ?? "").ToLower();
            var items = sol.contexto.TbPedidoM
                .Where(o => o.IdSolicitud == sol.TbSol.IdSolicitud)
                .Where(o => o.Dato != 2)
                .Where(o => o.Estatus == 3)
                .OrderBy(m => m.Id)
                .AsQueryable();

            return Json(new GridModelBuilder<TbPedidoM>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem =
                    () => sol.Get<TbPedidoM>(Convert.ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                Map = MaptoGridModel.MapToGridModel
            }.Build());
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
            var items = sol.contexto.TbPedidoM
                
                .Where(o => o.Dato != 2)
                .Where(o => o.Estatus == 3)
               
                .OrderBy(m => m.Id)
                .AsQueryable();

            return Json(new GridModelBuilder<TbPedidoM>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem =
                    () => sol.Get<TbPedidoM>(Convert.ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                Map = MaptoGridModel.MapToGridModel
            }.Build());
        }

        public ActionResult Edit(int id)
        {

         

            OIMEntity contextOimEntity = new OIMEntity();
            int id_ = contextOimEntity.TbPedidoM.FirstOrDefault(x=> x.Id== id).IdSolicitud  ;
            Session["IdSolicitud"] = id_;
            cSolicitud sol = new cSolicitud(id_, 1);
            var pedido = sol.GetPedidoM(id);
            var provedor = sol.contexto.TbProveedores.Where(x => x.Nombre == pedido.Proveedor);
            var clave = 0;
            if (provedor.Any())
                clave = provedor.FirstOrDefault().Id;
            if (string.IsNullOrWhiteSpace(pedido.Observacion))
                pedido.Observacion = "";
            ProveedorInput nuevo = new ProveedorInput()
            {
                id = pedido.Id,
                Autorizar = pedido.OrdenCompra,
                OtroProvedor = pedido.Proveedor,
                Proveedor = clave,
                Observacion = pedido.Observacion

            };
            nuevo.Nota = "";
            return PartialView(nuevo);
        }

        [HttpPost]
        public ActionResult Edit(ProveedorInput input)
        {
            if (!ModelState.IsValid)
                return PartialView("Create", input);
            if (input.Autorizar == 3 && input.Proveedor == 0 && string.IsNullOrWhiteSpace(input.OtroProvedor))
            {
                input.Nota = "<strong> Si selecciona otro proveedor debera de ingresar el nombre </strong>";
                return PartialView(input);
            }
            if (input.Autorizar == 2) //se rechazo
            {
                input.Proveedor = 0;
                input.OtroProvedor = "Rechazado";

            }
            if (input.Autorizar == 1) //Se 
            {
                input.Proveedor = 0;
                input.OtroProvedor = "Rechazado";

            }
            int id = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id, 1);
            if (!sol.Valido)
                return RedirectToAction("Index");
            sol.TbSol.EnviadoCom = 1;
            sol.UpdatePedido(input);


            // returning the key to call grid.api.update
            return Json(new { Id = input.id });
        }

        public ActionResult Edit1(int id)
        {
           
            int id_ = (int)Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id_, 1);
            var pedido=   sol.GetPedidoM(id);
            var provedor = sol.contexto.TbProveedores.Where(x => x.Nombre == pedido.Proveedor);
            var clave = 0;
            if(  provedor.Any())
                clave =provedor.FirstOrDefault().Id;
            if (string.IsNullOrWhiteSpace(pedido.Observacion))
                pedido.Observacion = "";
            ProveedorInput nuevo = new ProveedorInput()
            {
                id = pedido.Id,
                Autorizar = pedido.Autorizacion,
                OtroProvedor = pedido.Proveedor,
                Proveedor =  clave,
                Observacion = pedido.Observacion

            };
            nuevo.Nota = "";
            return PartialView(nuevo);
        }

        [HttpPost]
        public ActionResult Edit1(ProveedorInput input)
        {
            if (!ModelState.IsValid)
                return PartialView("Create", input);
            if (input.Autorizar == 1 && input.Proveedor == 0 && string.IsNullOrWhiteSpace(input.OtroProvedor))
            {
                input.Nota = "<strong> Si selecciona otro proveedor debera de ingresar el nombre </strong>";
                return PartialView(input);
            }
            if (input.Autorizar == 2)
            {
                input.OtroProvedor = "";

            }
           
            int id = (int) Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id, 1);
            if (!sol.Valido)
                return RedirectToAction("Index");
            sol.TbSol.EnviadoCom = 1;
            sol.UpdatePedido(input);


            // returning the key to call grid.api.update
            return Json(new {Id = input.id});
        }

        

        public ActionResult PorPedido()
        {
            ViewBag.Visible = 1;
            return View();


        }
        public ActionResult OrdenCompra()
        {
            OIMEntity contextOimEntity = new OIMEntity();
            var list = contextOimEntity.Sp_AllPedidoXEstatusXAdmin(3,3).OrderBy(x => x.Proveedor).ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult OrdenCompra(Sp_AllPedidoXEstatus_Result[] valor)
        {
            OIMEntity contextOimEntity = new OIMEntity();
            var list = contextOimEntity.Sp_AllPedidoXEstatusXAdmin(3, 3).OrderBy(x => x.Proveedor).ToList();
            return View(list);
        }
        public ActionResult prueba()
        {
            OIMEntity contextOimEntity = new OIMEntity();
            var list = contextOimEntity.Sp_AllPedidoXEstatusXAdmin(3, 3).OrderBy(x => x.Proveedor).ToList();
            return View(list);
        }

        public ActionResult Formato()
        {
           COrdenCompra compra = new COrdenCompra();
            compra.nuevo();
            ViewBag.Visible = true;
            return View(compra);
        }
    }

    public class OrdenCompController : Controller
    {
        public static object MapToGridModel(TbPedidoM o)
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
                    Categoria = o.TbCategoria.Nombre,
                    Seleccionado = o.TbOrdenCompra.Nombre,
                    Observacion = o.Observacion,
                    Solicitud = o.IdSolicitud,
                    Proveedor = o.Proveedor

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
            var items = sol.contexto.TbPedidoM

                .Where(o => o.Dato != 2)
                .Where(o => o.Estatus == 3)

                .OrderBy(m => m.Id)
                .AsQueryable();

            return Json(new GridModelBuilder<TbPedidoM>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem =
                    () => sol.Get<TbPedidoM>(Convert.ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                Map = MapToGridModel,

            }.Build());
        }
    }
}