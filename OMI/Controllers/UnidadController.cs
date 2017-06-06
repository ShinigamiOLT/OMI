using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OMI;

namespace OMI.Controllers
{
    public class UnidadController : Controller
    {
        private OPEntities db = new OPEntities();

        // GET: TbUnidads
        public ActionResult Index()
        {
            return View(db.TbUnidad.ToList());
        }

        // GET: TbUnidads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbUnidad tbUnidad = db.TbUnidad.Find(id);
            if (tbUnidad == null)
            {
                return HttpNotFound();
            }
            return View(tbUnidad);
        }

        // GET: TbUnidads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TbUnidads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre")] TbUnidad tbUnidad)
        {
            if (ModelState.IsValid)
            {
                db.TbUnidad.Add(tbUnidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbUnidad);
        }

        // GET: TbUnidads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbUnidad tbUnidad = db.TbUnidad.Find(id);
            if (tbUnidad == null)
            {
                return HttpNotFound();
            }
            return View(tbUnidad);
        }

        // POST: TbUnidads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre")] TbUnidad tbUnidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbUnidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbUnidad);
        }

        // GET: TbUnidads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbUnidad tbUnidad = db.TbUnidad.Find(id);
            if (tbUnidad == null)
            {
                return HttpNotFound();
            }
            return View(tbUnidad);
        }

        // POST: TbUnidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TbUnidad tbUnidad = db.TbUnidad.Find(id);
            db.TbUnidad.Remove(tbUnidad);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
