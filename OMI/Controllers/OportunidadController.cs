using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMI.Models;

namespace OMI.Controllers
{
    public class OportunidadController : Controller
    {
        // GET: Oportunidad
        public ActionResult Index()
        {
            OPEntities contexto = new OPEntities();
            TbOportunidad Tb = contexto.TbOportunidad.Find(1);
            cOportunidad opor = new cOportunidad();
            opor.oportunidad = Tb;
            return View(opor);
        }


        [HttpPost]
        public ActionResult Index(TbOportunidad nuevo)
        {
            return View(nuevo);
        }
    }
}