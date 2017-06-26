using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OIM_DAL;
using Omu.AwesomeMvc;

namespace OMI.Controllers
{
    public class IdUnidadLookupController : Controller
    {
        public OIMEntity contexto;

        public IdUnidadLookupController()
        {
            contexto = new OIMEntity();
        }
        public ActionResult GetItem(int? v)
        {
            var o =  contexto.TbUnidad.Find(v) ?? new TbUnidad() { Id = 1,Nombre = "Metros"};
            return Json(new KeyContent(o.Id, o.Nombre));
        }

        public ActionResult Search(string search, int page)
        {
            const int PageSize = 7;
            search = (search ?? "").ToLower().Trim();

            var list =contexto.TbUnidad.Where(f => (f.Nombre).ToLower().Contains(search)).OrderBy(x=>x.Id);
            List<KeyContent> ListaNueva = new List<KeyContent>();
            foreach (var unidad in list)
            {
                ListaNueva.Add(new KeyContent(unidad.Id,unidad.Nombre));
            }


            return Json(new AjaxListResult
            {
                Items =ListaNueva,// list.Skip((page - 1) * PageSize).Take(PageSize).Select(o => new KeyContent(o.Id, o.Nombre)),
                More = list.Count() > page * PageSize
            });
        }
    }
}