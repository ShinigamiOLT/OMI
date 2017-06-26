using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using OIM_DAL;

namespace OMI.Controllers
{
    public class OportunidadController : Controller
    {
        // GET: Oportunidad
        public ActionResult Index()
        {
            OIMEntity contexto = new OIMEntity();
            TbOportunidad Tb = contexto.TbOportunidad.Find(1);
            ViewBag.LAreas = contexto.TbAreaInteres.ToList();
            ViewBag.LTipoOportunidad = contexto.TbTipoOportunidad.ToList();
            ViewBag.Medios = new SelectList(contexto.TbMedioContacto.ToList(), "Id", "Nombre");
            Tb.Fecha = Tb.FechaSistema = DateTime.Now.Date;

            return View(Tb);
        }


        [HttpPost]
        public ActionResult Index(TbOportunidad nuevo, List<int> TipoOportunidad, List<int> AreasInteres)
        {
            OIMEntity contexto = new OIMEntity();

            nuevo.TbFormato = contexto.TbFormato.Find(nuevo.idFormato);
            nuevo.TbMedioContacto = contexto.TbMedioContacto.Find(nuevo.MedioContacto);
            nuevo.TbUsuario = contexto.TbUsuario.Find(nuevo.idUsuario);
        
            if (TipoOportunidad != null)
            {
            nuevo.TbTipoOportunidad.Clear();


                foreach (int i in TipoOportunidad)
                {
                  //  nuevo.TbTipoOportunidad.Add(contexto.TbTipoOportunidad.Find(i));
                   

                    TbTipoOportunidad x = contexto.TbTipoOportunidad.Find(i);
                  nuevo.TbTipoOportunidad.Add(x);

          
                }
            }   
           
            /*
            if (AreasInteres != null)
            {
              //  nuevo.TbAreaInteres.Clear();
                foreach (int i in AreasInteres)
                {
                    nuevo.TbAreaInteres.Add(contexto.TbAreaInteres.Find(i));
                    Console.WriteLine(i);
                }
            }
            */

            int clave = contexto.TbOportunidad.Max(x => x.Id);
            nuevo.Id = clave + 1;
            contexto.TbOportunidad.Add(nuevo);

            

            contexto.SaveChanges();

            ViewBag.LAreas = contexto.TbAreaInteres.ToList();
            ViewBag.LTipoOportunidad = contexto.TbTipoOportunidad.ToList();


            ViewBag.Medios = new SelectList(contexto.TbMedioContacto.ToList(), "Id", "Nombre");
            return View(nuevo);
        }

        public ActionResult Edit(int id = 1)
        {
            OIMEntity contexto = new OIMEntity();
            TbOportunidad opor = contexto.TbOportunidad.Include(a => a.TbTipoOportunidad).ToList()
                .Find(c => c.Id == id);
            if (opor == null)
                return HttpNotFound();
            ViewBag.LAreas = contexto.TbAreaInteres.ToList();
            ViewBag.LTipoOportunidad = contexto.TbTipoOportunidad.ToList();
            ViewBag.Medios = new SelectList(contexto.TbMedioContacto.ToList(), "Id", "Nombre");
            return View(opor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TbOportunidad oportunidad, List<int> TipoOportunidad, List<int> AreasInteres,string Option)
        {


            OIMEntity contexto = new OIMEntity();
            oportunidad.TbFormato = contexto.TbFormato.Find(oportunidad.idFormato);
            oportunidad.TbMedioContacto = contexto.TbMedioContacto.Find(oportunidad.MedioContacto);
            oportunidad.TbUsuario = contexto.TbUsuario.Find(oportunidad.idUsuario);
            oportunidad.Enviado = 0;
            if (Option == "Enviar")
            {
                oportunidad.Enviado = 1;
            }
            contexto.Entry(oportunidad).State = EntityState.Modified;
            contexto.SaveChanges();
            TbOportunidad recuperado = contexto.TbOportunidad.Include(a => a.TbTipoOportunidad).Include(x=>x.TbAreaInteres).ToList()
                .Find(c => c.Id == oportunidad.Id);
 recuperado.TbTipoOportunidad.Clear();
            if(TipoOportunidad!=null)
            {

            foreach (int i in TipoOportunidad)
            {
                TbTipoOportunidad x = contexto.TbTipoOportunidad.Find(i);
                recuperado.TbTipoOportunidad.Add(x);
            }
  
            }          contexto.SaveChanges();
      recuperado.TbAreaInteres.Clear();
            if (AreasInteres != null)
            {
          

                foreach (int i in AreasInteres)
                {
                    TbAreaInteres x = contexto.TbAreaInteres.Find(i);
                    recuperado.TbAreaInteres.Add(x);
                }

             
            }
           
            contexto.SaveChanges();
            ViewBag.LAreas = contexto.TbAreaInteres.ToList();
            ViewBag.LTipoOportunidad = contexto.TbTipoOportunidad.ToList();
            ViewBag.Medios = new SelectList(contexto.TbMedioContacto.ToList(), "Id", "Nombre");

            return View(oportunidad);
        }

        public ActionResult List()
        {
            OIMEntity contexto = new OIMEntity();
            var lista =contexto.TbOportunidad.Include(a => a.TbTipoOportunidad).ToList();
            return View(lista);
        }

        public ActionResult Create()
        {
            OIMEntity contexto = new OIMEntity();
            TbOportunidad Tb = new TbOportunidad();
            Tb.TbFormato = contexto.TbFormato.Find(9);
            Tb.TbUsuario = contexto.TbUsuario.Find(2);
            Tb.idUsuario = Tb.TbUsuario.Id;
            Tb.idFormato = Tb.TbFormato.Id;
            ViewBag.LAreas = contexto.TbAreaInteres.ToList();
            ViewBag.LTipoOportunidad = contexto.TbTipoOportunidad.ToList();
            ViewBag.Medios = new SelectList(contexto.TbMedioContacto.ToList(), "Id", "Nombre");
            int clave = 0;
            if( contexto.TbOportunidad.Any())
          clave=  contexto.TbOportunidad.Max(x => x.Id);
            Tb.Id = clave + 1;
            Tb.Folio = Tb.TbFormato.Nombre +"-"+ Tb.Id.ToString("0000");
            Tb.Fecha = Tb.FechaSistema = DateTime.Now.Date;
            return View(Tb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TbOportunidad oportunidad, List<int> TipoOportunidad, List<int> AreasInteres, int idUsuario, DateTime Fecha, String Option)
        {
        
            
            oportunidad.FechaSistema = DateTime.Now;
            OIMEntity contexto = new OIMEntity();

         TbOportunidad temp =   contexto.TbOportunidad.Find(oportunidad.Id);

            if (temp == null)
            {

                oportunidad.TbFormato = contexto.TbFormato.Find(oportunidad.idFormato);
                oportunidad.TbMedioContacto = contexto.TbMedioContacto.Find(oportunidad.MedioContacto);
                oportunidad.TbUsuario = contexto.TbUsuario.Find(oportunidad.idUsuario);
                contexto.TbOportunidad.Add(oportunidad);
                contexto.SaveChanges();
                TbOportunidad recuperado = contexto.TbOportunidad.Include(a => a.TbTipoOportunidad)
                    .Include(x => x.TbAreaInteres).ToList()
                    .Find(c => c.Id == oportunidad.Id);

                if (TipoOportunidad != null)
                {
                    recuperado.TbTipoOportunidad.Clear();
                    foreach (int i in TipoOportunidad)
                    {
                        TbTipoOportunidad x = contexto.TbTipoOportunidad.Find(i);
                        recuperado.TbTipoOportunidad.Add(x);
                    }

                }
                if (AreasInteres != null)
                {
                    recuperado.TbAreaInteres.Clear();

                    foreach (int i in AreasInteres)
                    {
                        TbAreaInteres x = contexto.TbAreaInteres.Find(i);
                        recuperado.TbAreaInteres.Add(x);
                    }

                    contexto.SaveChanges();
                }

                if (Option == "Borrador")
                {
                 return   RedirectToAction("Edit", new {id = oportunidad.Id});
                }
               
            }

            else
            {
                //aqui se supone que si existe esa tabla.
               
            }
            ViewBag.LAreas = contexto.TbAreaInteres.ToList();
                ViewBag.LTipoOportunidad = contexto.TbTipoOportunidad.ToList();
                ViewBag.Medios = new SelectList(contexto.TbMedioContacto.ToList(), "Id", "Nombre");
         
            if (ModelState.IsValid)
                return RedirectToAction("List");
            return View(oportunidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Borrador(TbOportunidad oportunidad, List<int> TipoOportunidad, List<int> AreasInteres, int idUsuario, DateTime Fecha, String Option)
        {


            oportunidad.FechaSistema = DateTime.Now;
            OIMEntity contexto = new OIMEntity();
            oportunidad.TbFormato = contexto.TbFormato.Find(oportunidad.idFormato);
            oportunidad.TbMedioContacto = contexto.TbMedioContacto.Find(oportunidad.MedioContacto);
            oportunidad.TbUsuario = contexto.TbUsuario.Find(oportunidad.idUsuario);

            contexto.TbOportunidad.Add(oportunidad);
            contexto.SaveChanges();
            TbOportunidad recuperado = contexto.TbOportunidad.Include(a => a.TbTipoOportunidad).Include(x => x.TbAreaInteres).ToList()
                .Find(c => c.Id == oportunidad.Id);

            if (TipoOportunidad != null)
            {
                recuperado.TbTipoOportunidad.Clear();
                foreach (int i in TipoOportunidad)
                {
                    TbTipoOportunidad x = contexto.TbTipoOportunidad.Find(i);
                    recuperado.TbTipoOportunidad.Add(x);
                }

            }
            if (AreasInteres != null)
            {
                recuperado.TbAreaInteres.Clear();

                foreach (int i in AreasInteres)
                {
                    TbAreaInteres x = contexto.TbAreaInteres.Find(i);
                    recuperado.TbAreaInteres.Add(x);
                }

                contexto.SaveChanges();
            }
            ViewBag.LAreas = contexto.TbAreaInteres.ToList();
            ViewBag.LTipoOportunidad = contexto.TbTipoOportunidad.ToList();
            ViewBag.Medios = new SelectList(contexto.TbMedioContacto.ToList(), "Id", "Nombre");
            if (ModelState.IsValid)
                return RedirectToAction("List");
            return   RedirectToAction("Create", new { oportunidad});
        }




    }


}