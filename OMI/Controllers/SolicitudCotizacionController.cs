using System;
using System.Linq;
using System.Web.Mvc;
using OIM_DAL;
using Omu.AwesomeMvc;
using OMI.Models;

namespace OMI.Controllers
{
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
            cSolCotizacion sol = new cSolCotizacion();
            sol.Id = id;
            sol.nuevo(4,0);


            search = (search ?? "").ToLower();
            var items = sol.contexto.TbPedidoM.Where(x => x.Id == sol.TbSolicitud.IdPedido).AsQueryable();


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