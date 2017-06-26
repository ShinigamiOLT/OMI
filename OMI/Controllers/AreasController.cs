using OMI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OIM_DAL;

namespace OMI.Controllers
{
    public class AreasController : Controller
    {
        private UtilApp util;

        public AreasController()
        {
           util  = new UtilApp();
        }
        // GET: Areas
        public ActionResult Index()
        {
          

            return View(util.GetAreas());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TbArea Areas)
        {
            if (ModelState.IsValid)
            {
                util.Insert(Areas);
            return   RedirectToAction("Index");
            }

            return View(Areas);
        }
        public ActionResult Edit(int id)
        {
           
            return View(util.GetArea(id));
        }

        [HttpPost]
        public ActionResult Edit(TbArea Areas)
        {
            if (ModelState.IsValid)
            {
                util.Update(Areas);
             return    RedirectToAction("Index");
            }

            return View(Areas);
        }

        public ActionResult Delete(int id)
        {

          
            TbArea regiones = util.entidad.TbArea.Find(id);
            if (regiones != null)
            {
                util.entidad.TbArea.Remove(regiones);
                util.entidad.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public  JsonResult DeleteConfirmed(string id)
        {
            try
            {
                var ident = !string.IsNullOrEmpty(id) ? int.Parse(id) : 0;

                TbArea regiones = util.entidad.TbArea.Find(ident);
                if (regiones != null)
                {
                    util.entidad.TbArea.Remove(regiones);
                    util.entidad.SaveChanges();
                }
                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, error = ex.Message });
            }
        }


    }
}