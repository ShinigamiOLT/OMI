using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using OMI.Models;
using OMI.Models.Utils;

namespace OMI.Controllers
{
    public class AdmonController : Controller
    {
        // GET: Admon
        public ActionResult SolCotizacion()
        {
            OPEntities context = new OPEntities();

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

        public ActionResult GridGetItems(GridParams g, string search)
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
                .Where(o=> o.Estatus==3)
                .OrderBy(m=>m.Id)
                .AsQueryable();

            return Json(new GridModelBuilder<TbPedidoM>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem =
                    () => sol.Get<TbPedidoM>(Convert.ToInt32(g.Key),
                        sol.TbSol.IdSolicitud), // called by the grid.api.update ( edit popupform success js func )
                Map = MaptoGridModel.MapToGridModel
            }.Build());
        }
    }
}