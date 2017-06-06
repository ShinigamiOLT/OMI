using OMI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMI.Controllers
{
    public class AreasController : Controller
    {
        // GET: Areas
        public ActionResult Index()
        {
            UtilApp util = new UtilApp();

            return View(util.GetAreas());
        }
    }
}