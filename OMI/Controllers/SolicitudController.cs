using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMI.Models;

namespace OMI.Controllers
{
    public class SolicitudController : Controller
    {
        // GET: Solicitud
        public ActionResult Index()
        {
            return View();
        }

        public string Datos()
        {
            cSolicitud sol = new cSolicitud();

            return sol.Datos();
        }
    }
}