using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMI.Models;

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
    
    }
}