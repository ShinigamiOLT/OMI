using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OIM_DAL;
using OMI;

namespace OMI.Controllers
{
    public class TbOportunidadsController : Controller
    {
        private OIMEntity db = new OIMEntity();

        // GET: TbOportunidads
        public ActionResult Index()
        {
            var tbOportunidad = db.TbOportunidad.Include(t => t.TbFormato).Include(t => t.TbMedioContacto).Include(t => t.TbUsuario);
            return View(tbOportunidad.ToList());
        }

        // GET: TbOportunidads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbOportunidad tbOportunidad = db.TbOportunidad.Find(id);
            if (tbOportunidad == null)
            {
                return HttpNotFound();
            }
            return View(tbOportunidad);
        }

        // GET: TbOportunidads/Create
        public ActionResult Create()
        {
            ViewBag.idFormato = new SelectList(db.TbFormato, "Id", "Nombre");
            ViewBag.MedioContacto = new SelectList(db.TbMedioContacto, "Id", "Nombre");
            ViewBag.idUsuario = new SelectList(db.TbUsuario, "Id", "Nombre");
            return View();
        }

        // POST: TbOportunidads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Folio,Fecha,FechaSistema,Representante,Telefono,Correo,MedioContacto,Descripcion,idFormato,idUsuario,Compania")] TbOportunidad tbOportunidad)
        {
            if (ModelState.IsValid)
            {
                db.TbOportunidad.Add(tbOportunidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idFormato = new SelectList(db.TbFormato, "Id", "Nombre", tbOportunidad.idFormato);
            ViewBag.MedioContacto = new SelectList(db.TbMedioContacto, "Id", "Nombre", tbOportunidad.MedioContacto);
            ViewBag.idUsuario = new SelectList(db.TbUsuario, "Id", "Nombre", tbOportunidad.idUsuario);
            return View(tbOportunidad);
        }

        // GET: TbOportunidads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbOportunidad tbOportunidad = db.TbOportunidad.Find(id);
            if (tbOportunidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.idFormato = new SelectList(db.TbFormato, "Id", "Nombre", tbOportunidad.idFormato);
            ViewBag.MedioContacto = new SelectList(db.TbMedioContacto, "Id", "Nombre", tbOportunidad.MedioContacto);
            ViewBag.idUsuario = new SelectList(db.TbUsuario, "Id", "Nombre", tbOportunidad.idUsuario);
            return View(tbOportunidad);
        }

        // POST: TbOportunidads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Folio,Fecha,FechaSistema,Representante,Telefono,Correo,MedioContacto,Descripcion,idFormato,idUsuario,Compania")] TbOportunidad tbOportunidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbOportunidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idFormato = new SelectList(db.TbFormato, "Id", "Nombre", tbOportunidad.idFormato);
            ViewBag.MedioContacto = new SelectList(db.TbMedioContacto, "Id", "Nombre", tbOportunidad.MedioContacto);
            ViewBag.idUsuario = new SelectList(db.TbUsuario, "Id", "Nombre", tbOportunidad.idUsuario);
            return View(tbOportunidad);
        }

        // GET: TbOportunidads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbOportunidad tbOportunidad = db.TbOportunidad.Find(id);
            if (tbOportunidad == null)
            {
                return HttpNotFound();
            }
            return View(tbOportunidad);
        }

        // POST: TbOportunidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TbOportunidad tbOportunidad = db.TbOportunidad.Find(id);
            db.TbOportunidad.Remove(tbOportunidad);
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
