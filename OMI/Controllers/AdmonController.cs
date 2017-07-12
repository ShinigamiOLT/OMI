using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OIM_DAL;
using Omu.AwesomeMvc;
using OMI.Models;
using OMI.Models.Utils;
using  System.Data.Entity;
using System.Data.Entity.Core.Objects;

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
                return RedirectToAction("Index","Home");
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
                return RedirectToAction("Index","Home");
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
                    () => sol.Get<TbPedidoM>(Convert
                        .ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                Map = MaptoGridModel.MapToGridModel
            }.Build());
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            int id = 1;
            if (Session["IdSolicitud"] != null)
                id = (int) Session["IdSolicitud"];
            cSolicitud sol = new cSolicitud(id, 1);
            if (!sol.Valido)
                return RedirectToAction("Index","Home");
            search = (search ?? "").ToLower();
            var items = sol.contexto.TbPedidoM

                .Where(o => o.Dato != 2)
                .Where(o => o.Estatus == 3)
                .Where(o=>o.ConfirmaOrden==0)
                .OrderBy(m => m.Id)
                .AsQueryable();

            return Json(new GridModelBuilder<TbPedidoM>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem =
                    () => sol.Get<TbPedidoM>(Convert
                        .ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                Map = MaptoGridModel.MapToGridModel
            }.Build());
        }

        public ActionResult Edit(int id)
        {



            OIMEntity contextOimEntity = new OIMEntity();
            int id_ = contextOimEntity.TbPedidoM.FirstOrDefault(x => x.Id == id).IdSolicitud.Value;
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
            if ((input.Autorizar == 3 || input.Autorizar == 1) && input.Proveedor == 0 &&
                string.IsNullOrWhiteSpace(input.OtroProvedor))
            {
                input.Nota = "<strong> Si selecciona otro proveedor debera de ingresar el nombre </strong>";
                return PartialView(input);
            }
            if (input.Autorizar == 2 || input.Autorizar == 4) //se rechazo
            {
                input.Proveedor = 0;
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

        public ActionResult Edit1(int id)
        {

            int id_ = (int) Session["IdSolicitud"];
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
                Autorizar = pedido.Autorizacion,
                OtroProvedor = pedido.Proveedor,
                Proveedor = clave,
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
            var list = contextOimEntity.Sp_AllPedidoXEstatusXAdmin(3, (int) eOrdenCompra.Autorizado)
                .OrderBy(x => x.Proveedor).ToList();
           /* list.AddRange(contextOimEntity.Sp_AllPedidoXEstatusXAdmin(3, (int) eOrdenCompra.SeleccionCotizacion)
                .OrderBy(x => x.Proveedor).ToList());*/

            return View(list);
        }

        [HttpPost]
        public ActionResult OrdenCompra(int[] reglon)
        {

            if (reglon != null)
                if (reglon.Length > 0)
                {

                    if (ObtienePedido(reglon))
                    {


                        //implica que hay mas de un valor.
                        COrdenCompra compranueva = new COrdenCompra();
                        {
                            compranueva.nuevo(6);
                            compranueva.ListaPedidos(reglon);


                            return RedirectToAction("Formato", new {Id = compranueva.Id});
                        }
                    }
                }

            OIMEntity contextOimEntity = new OIMEntity();
            var list = contextOimEntity.Sp_AllPedidoXEstatusXAdmin(3, (int) eOrdenCompra.Autorizado)
                .OrderBy(x => x.Proveedor).ToList();
           /* list.AddRange(contextOimEntity.Sp_AllPedidoXEstatusXAdmin(3, (int) eOrdenCompra.SeleccionCotizacion)
                .OrderBy(x => x.Proveedor).ToList());*/
            return View(list);
        }

        public ActionResult SolicitudCotizacion()
        {
            OIMEntity contextOimEntity = new OIMEntity();
            ViewBag.Visible = 1;
            var list = contextOimEntity.Sp_AllPedidoXEstatusXAdmin(3, (int)eOrdenCompra.SeleccionCotizacion)
                .OrderBy(x => x.Proveedor).ToList();


            return View(list);
        }

        [HttpPost]
        public ActionResult SolicitudCotizacion(int[] reglon)
        {

            if (reglon != null)
                if (reglon.Length > 0)
                {

                    if (ObtienePedido(reglon))
                    {


                        //implica que hay mas de un valor.
                        COrdenCompra compranueva = new COrdenCompra();
                        {
                            compranueva.nuevo(4);
                            compranueva.ListaPedidos(reglon);
                            return RedirectToAction("FormatoSolCotizacion", new { Id = compranueva.Id });
                        }
                    }
                }

            OIMEntity contextOimEntity = new OIMEntity();
            var list = contextOimEntity.Sp_AllPedidoXEstatusXAdmin(3, (int)eOrdenCompra.SeleccionCotizacion)
                .OrderBy(x => x.Proveedor).ToList();
            ViewBag.Visible = 1;
            return View(list);
        }



        private bool ObtienePedido(int[] reglon)
        {
            //bosquemos los pedidos y vemos si pertenece alos mismos provedores
            using (OIMEntity contexto = new OIMEntity())
            {

                List<string> Provedores = new List<string>();
                foreach (int i in reglon)
                {
                    var elemento = contexto.TbPedidoM.Find(i);
                    string nombre = "";
                    if (elemento.IdProveedor == 0)
                    {
                        nombre = elemento.Proveedor;

                    }
                    else
                    {
                        nombre = elemento.TbProveedores.Nombre;
                    }

                    if (!Provedores.Contains(nombre))
                    {
                        Provedores.Add(nombre);
                    }


                }

                return (Provedores.Count <= 1);
            }

        }

        public ActionResult prueba()
        {
            OIMEntity contextOimEntity = new OIMEntity();
            var list = contextOimEntity.Sp_AllPedidoXEstatusXAdmin(3, (int) eOrdenCompra.Autorizado)
                .OrderBy(x => x.Proveedor).ToList();
            list.AddRange(contextOimEntity.Sp_AllPedidoXEstatusXAdmin(3, (int) eOrdenCompra.SeleccionCotizacion)
                .OrderBy(x => x.Proveedor).ToList());
            return View(list);
        }

        public ActionResult Formato(int Id)
        {

            COrdenCompra compra = new COrdenCompra();

            compra.Id = Id;
            compra.nuevo(6);
            ViewBag.Visible = compra.EstadoEnvio();

            ViewBag.Valor = 200;
            Session["IdCompra"] = compra.Id;
            return View(compra);
        }

        [HttpPost]

        public ActionResult Formato(COrdenCompra compra, string Opcion)
        {
            if ( Opcion == "Cancelar")
            {

                // si se cancela borramos los valores del idcompra y retornamos a la venta de Orden de Compra
                compra.EliminaOrden();
                return RedirectToAction("OrdenCompra");
            }


            compra.SalvarDatos();
            ViewBag.Visible = false;
            return View(compra);
        }

        [HttpPost]
        public JsonResult Sumatoria(int id)
        {
            OIMEntity contexto = new OIMEntity();
            var valor = (contexto.Sp_Suma(id).FirstOrDefault());

            NumLetra ele = new NumLetra();
            string valor1 = valor + " " + ele.Convertir(valor.Value.ToString(), false);
            return Json(valor1);
        }

        public ActionResult Historial()

        {
            OIMEntity contexto = new OIMEntity();
          var lista =  contexto.Sp_Historial();
            return View(lista);
        }

        public ActionResult FolioCompra(int Id)
        {
            return RedirectToAction("Formato", "Admon",new {Id});
        }
        public ActionResult FolioSolicitud(int Id)
        {
            return RedirectToAction("NuevaSolicitud","Solicitud", new {Id});
        }

        public ActionResult DetallePedido(int Id)
        {
//mandemos a ver el pedido
OIMEntity contexto= new OIMEntity();
           var pedido= contexto.TbPedidoM.Find(Id);
            return View(pedido);

        }

        public ActionResult Surtir(int Id)
        {
            OIMEntity contexto = new OIMEntity();
            var pedido = contexto.TbPedidoM.Find(Id);
            pedido.Surtido = 1;
            pedido.Estatus = (int)StatusInfra.Inventario;
            
            contexto.SaveChanges();
            return RedirectToAction("Historial");
        }
        public ActionResult FormatoSolCotizacion(int Id)
        {

            COrdenCompra compra = new COrdenCompra();

            compra.Id = Id;
            compra.nuevo(4);
            ViewBag.Visible = compra.EstadoEnvio();

            ViewBag.Valor = 200;
            Session["IdCompra"] = compra.Id;
            return View(compra);
        }

        [HttpPost]
        public ActionResult FormatoSolCotizacion(COrdenCompra compra, string Opcion)
        {
            if (Opcion == "Cancelar")
            {

                // si se cancela borramos los valores del idcompra y retornamos a la venta de Orden de Compra
                compra.EliminaOrden();
                return RedirectToAction("OrdenCompra");
            }


            compra.SalvarDatos();
            ViewBag.Visible = false;
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
                    Numero = 1,
                    o.Id,
                    Categoria = o.TbCategoria.Nombre,
                    o.Cantidad,
                    Unidad = o.TbUnidad.Nombre,
                    o.Descripcion,



                    Seleccionado = o.TbOrdenCompra!=null ? o.TbOrdenCompra.Nombre:"",
                    Precio = o.Precio,
                    Importe = o.Importe,
                    Observacion = o.Observacion

                };
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {

            int id = 1;
            if (Session["IdCompra"] != null)
                id = (int) Session["IdCompra"];
            COrdenCompra sol = new COrdenCompra();
            sol.Id = id;
            sol.nuevo(6);


            search = (search ?? "").ToLower();
            var items = sol.contexto.TbPedidoM
                .Where(x => x.IdOrdenCompra != null)
                .Where(x => ((int) x.IdOrdenCompra) == id)

                .OrderBy(m => m.Id)
                .AsQueryable();


            return Json(new GridModelBuilder<TbPedidoM>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem =
                    () => sol.GetPedidoM(Convert
                        .ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                Map = MapToGridModel

            }.Build());
        }

        public ActionResult Edit(int Id)
        {
            OIMEntity contexto = new OIMEntity();
            PrecioUnitarioInput precio = new PrecioUnitarioInput();
            var elemnto = contexto.TbPedidoM.Find(Id);
            if (elemnto != null)
            {
                precio.Precio = elemnto.Precio ??0;
                precio.Id = Id;
            }
            return PartialView(precio);
        }

        [HttpPost]
        public ActionResult Edit(PrecioUnitarioInput input)
        {
            OIMEntity contexto = new OIMEntity();

            var elemnto = contexto.TbPedidoM.Find(input.Id);
            if (elemnto != null)
            {
                elemnto.Precio = input.Precio;
                elemnto.Importe = elemnto.Cantidad * input.Precio;
            }
            contexto.SaveChanges();

            ViewBag.Valor = 100;
            return Json(new {Id = input.Id});
        }

        public ActionResult Delete(int id, string gridId)
        {

            OIMEntity contexto = new OIMEntity();

            var dinner = contexto.TbPedidoM.Find(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                GridId = gridId,
                Message = string.Format("¿Estas Seguro que quieres Eliminar el pedido de <b>{0}</b> ?",
                    dinner.Descripcion)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            OIMEntity contexto = new OIMEntity();

            var dinner = contexto.TbPedidoM.Find(input.Id);
            //este eliminar es virtual xk lo que queremos es desanclar al numero de orden
            // contexto.TbPedidoM.Remove(dinner);
            dinner.IdOrdenCompra = null;
            contexto.Entry(dinner).State = EntityState.Modified;
            contexto.SaveChanges();
            return Json(new {Id = input.Id});
        }
    }


    public class SolicitudCotizacionController : Controller
    {
        public static object MapToGridModel(TbPedidoM o)
        {
            return
                new
                {
                    Numero = 1,
                    o.Id,
                    Categoria = o.TbCategoria.Nombre,
                    o.Cantidad,
                    Unidad = o.TbUnidad.Nombre,
                    o.Descripcion,



                    Seleccionado = o.TbOrdenCompra != null ? o.TbOrdenCompra.Nombre : "",
                    Precio = o.Precio,
                    Importe = o.Importe,
                    Observacion = o.Observacion

                };
        }
        public ActionResult GridGetItems(GridParams g, string search)
        {

            int id = 1;
            if (Session["IdCompra"] != null)
                id = (int)Session["IdCompra"];
            COrdenCompra sol = new COrdenCompra();
            sol.Id = id;
            sol.nuevo(4);


            search = (search ?? "").ToLower();
            var items = sol.contexto.TbPedidoM
                .Where(x => x.IdOrdenCompra != null)
                .Where(x => ((int)x.IdOrdenCompra) == id)

                .OrderBy(m => m.Id)
                .AsQueryable();


            return Json(new GridModelBuilder<TbPedidoM>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem =
                    () => sol.GetPedidoM(Convert
                        .ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                Map = MapToGridModel

            }.Build());
        }

    }

}