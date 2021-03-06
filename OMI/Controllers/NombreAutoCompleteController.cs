using System.Linq;
using System.Web.Mvc;
using OIM_DAL;
using Omu.AwesomeMvc;

namespace OMI.Controllers
{
    public class NombreAutoCompleteController : Controller
    {
        public ActionResult GetItems(string v)
        {
            v = (v ?? "").ToLower().Trim();
            OIMEntity contexto = new OIMEntity();
            var items = contexto.TbProveedores.Where(o => o.Nombre.ToLower().Contains(v));
            return Json(items.Select(o => new KeyContent(o.Id, o.Nombre)));
        }
    }
}