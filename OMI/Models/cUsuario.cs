using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using OMI.Models.Utils;

namespace OMI.Models
{
    public class cUsuario
    {
        [Key]
        public string Id { get; set; }
        [DisplayName("Nombre")]
        public string FullName { get; set; }
        [DisplayName("Apellido")]
        public string Apellido { get; set; }
        [DisplayName("Usuario")]
        public string Usuario { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
    }

    public abstract class CatalogoController<TEntity, TEntityViewModel, TContext> : Controller where TEntity : class
        where TEntityViewModel : class, new()
        where TContext : DbContext, new()
    {
        protected readonly TContext Db = new TContext();
        private string _label;

        protected string Label
        {
            get { return _label; }
            set { _label = value.ToLower(); }
        }

        protected bool LabelMasc { get; set; }
        protected string PropertyKey { get; set; }

        protected bool PropertyKeyMasc { get; set; }

        // GET: <TEntity>
        public virtual ActionResult Index()
        {
            return View();
        }

        // GET: <TEntity>/Details/<id>
        public virtual ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entityObj = Db.Set<TEntity>().Find(id);
            if (entityObj == null)
            {
                return HttpNotFound();
            }
            return View(entityObj);
        }

        // GET: <TEntity>/Create
        public virtual ActionResult Create()
        {
            CreateViewBagLists();
            return View();
        }

        // POST: <TEntity>/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(TEntity entityObj)
        {
            CreateViewBagLists();
            if (ModelState.IsValid)
            {
                try
                {
                    Db.Set<TEntity>().Add(entityObj);
                    Db.SaveChanges();
                    string labelKey = entityObj.GetType().GetProperty(PropertyKey).GetValue(entityObj, null).ToString();
                    MessageToIndexManager
                        .SetMessage(
                            TempData,
                            string.Format("{0} {1} <strong>{2}</strong> fue {3} con éxito",
                                (LabelMasc ? "El" : "La"),
                                Label,
                                labelKey,
                                (LabelMasc ? "creado" : "creada")
                            ),
                            MessageToIndexType.Success
                        );
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.InnerException.InnerException as SqlException;
                    if (innerException != null && innerException.Number == 2627)
                    {
                        ModelState
                            .AddModelError(
                                "",
                                string.Format("Ya existe {0} {1} con {2} {3}",
                                    (LabelMasc ? "un" : "una"),
                                    Label,
                                    (PropertyKeyMasc ? "ese" : "esa"),
                                    PropertyKey.ToLower()
                                )
                            );
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(entityObj);
        }

        // GET: <TEntity>/Edit/<id>
        public virtual ActionResult Edit(string id)
        {
            CreateViewBagLists();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entityObj = Db.Set<TEntity>().Find(id);
            if (entityObj == null)
            {
                return HttpNotFound();
            }
            return View(entityObj);
        }

        // POST: <TEntity>/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TEntity entityObj)
        {
            CreateViewBagLists();
            if (ModelState.IsValid)
            {
                Db.Entry(entityObj).State = EntityState.Modified;
                Db.SaveChanges();
                string labelKey = entityObj.GetType().GetProperty(PropertyKey).GetValue(entityObj, null).ToString();
                MessageToIndexManager
                    .SetMessage(
                        TempData,
                        string.Format("{0} {1} <strong>{2}</strong> fue {3} con éxito",
                            (LabelMasc ? "El" : "La"),
                            Label,
                            labelKey,
                            (LabelMasc ? "editado" : "editada")
                        ),
                        MessageToIndexType.Success
                    );
                return RedirectToAction("Index");
            }
            return View(entityObj);
        }


        // POST: <TEntity>/Delete/<id>
        [HttpPost, ActionName("Delete")]
        public virtual JsonResult DeleteConfirmed(string id)
        {
            try
            {
                var entityObj = Db.Set<TEntity>().Find(id);
                if (entityObj != null)
                {
                    Db.Set<TEntity>().Remove(entityObj);
                    Db.SaveChanges();
                }
                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, error = ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        ///* Foreign Keys related functions */

        //public IEnumerable<TU> GetEntitySetEnumerable<TU>() where TU : class
        //{
        //    return Db.Set<TU>().Select(p =>
        //    {
        //        return 
        //    }).ToList();
        //}

        //public IEnumerable<string> GetFkPropertyNames()
        //{
        //    ObjectContext objectContext = ((IObjectContextAdapter)Db).ObjectContext;
        //    ObjectSet<TEntity> set = objectContext.CreateObjectSet<TEntity>();
        //    return set.EntitySet.ElementType.NavigationProperties
        //        .Where(p=>p.GetDependentProperties().Any())
        //        .Select(p => p.Name);
        //}

        //protected virtual void CreateViewBagLists()
        //{
        //    var fks = GetFkPropertyNames();
        //    fks.ForEach(p =>
        //    {
        //        MethodInfo method = this.GetType().GetMethod("GetEntitySetEnumerable");
        //        Type myType = Type.GetType("Proyecto_DAL."+p+", Proyecto_DAL");
        //        MethodInfo generic = method.MakeGenericMethod(myType);
        //        ViewData.Add(p + "List", generic.Invoke(this, null));
        //    });
        //}
        protected virtual void CreateViewBagLists()
        {
        }

        /* Grid-related functions */

        protected virtual TEntityViewModel FillViewModel(TEntity entityObj)
        {
            return new TEntityViewModel();
        }

        protected virtual IQueryable<TEntityViewModel> Datasource()
        {
            return Db.Set<TEntity>().AsEnumerable().Select(FillViewModel).AsQueryable();

        }
        protected virtual List<TEntityViewModel> FilterDataToExport(DTParameters param)
        {
            //var dtsource = Db.Set<TEntity>().AsEnumerable().Select(FillViewModel).AsQueryable();
            var dtsource = Datasource();
            var filteredData = FilterData(param, dtsource);

            return filteredData
                .ToList();
        }

        protected virtual IQueryable<TEntityViewModel> FilterData(DTParameters param,
            IQueryable<TEntityViewModel> source)
        {
            return ResultSet<TEntityViewModel>
                .GetFilteredOrderedResult(param.Search.Value, param.SortOrder, source);
        }

        [HttpPost]
        public virtual JsonResult DataHandler(DTParameters param)
        {
            try
            {
                //  var dtsource = Db.Set<TEntity>().AsEnumerable().Select(FillViewModel).AsQueryable();
                var dtsource = Datasource();

                var filteredData = FilterData(param, dtsource);

                var count = filteredData.Count();

                List<TEntityViewModel> data = Queryable.Take<TEntityViewModel>(filteredData
                        .Skip(param.Start), param.Length)
                    .ToList();

                DTResult<TEntity, TEntityViewModel> result = new DTResult<TEntity, TEntityViewModel>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = dtsource.Count()
                };

                var jsonResult = Json(result);
                jsonResult.MaxJsonLength = int.MaxValue;

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        /* FILE EXPORTING FUNCTIONS */

        protected enum GenerateFileType
        {
            Pdf,
            Excel
        }

        protected virtual FileResult GenerateFile(string paramObj, GenerateFileType type)
        {
            var param = JsonConvert.DeserializeObject<DTParameters>(paramObj);
            // generate the filtered and ordered data
            var data = FilterDataToExport(param);
            IExporter exporterIns;
            var bytes = new byte[] { };

            string extension, mimeType;
            switch (type)
            {
                case GenerateFileType.Excel:
                    exporterIns = new ExcelExporter();
                    extension = "xlsx";
                    mimeType = "application/vnd.ms-excel";
                    break;
                case GenerateFileType.Pdf:
                    exporterIns = new PdfExporter();
                    extension = "pdf";
                    mimeType = "application/pdf";
                    break;
                default:
                    exporterIns = null;
                    extension = "txt";
                    mimeType = "text/plain";
                    break;
            }

            if (exporterIns != null)
            {
                // create the document as memory stream, then convert to byte array
                using (var exportData = exporterIns.DTCatalogosExport(data, param.Columns))
                {
                    bytes = exportData.ToArray();
                }
            }
            string saveAsFileName = string.Format("{1}-{0:d}.{2}", DateTime.Now, Label, extension).Replace("/", "-");
            return File(bytes, mimeType, saveAsFileName);
        }

        [HttpPost]
        public virtual FileResult DownloadXLS(string paramObj)
        {
            return GenerateFile(paramObj, GenerateFileType.Excel);
        }

        [HttpPost]
        public virtual FileResult DownloadPDF(string paramObj)
        {
            return GenerateFile(paramObj, GenerateFileType.Pdf);
        }
    }

}