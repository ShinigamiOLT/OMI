using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using OIM_DAL;
using Omu.AwesomeMvc;
using OMI.Models;

namespace OMI.Controllers
{
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
}