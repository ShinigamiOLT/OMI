using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMI.Controllers
{
    public class OrganizacionController : Controller
    {
        // GET: Organizacion
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Reglamento()
        {
            return View();
        }
        public ActionResult Politica()
        {
            return View();
        }
        public ActionResult Maestra()
        {
            OPEntities contexto = new OPEntities();
            return View(contexto.TablaMaestra.ToList());
        }
    }
}