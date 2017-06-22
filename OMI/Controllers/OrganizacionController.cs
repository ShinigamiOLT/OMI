using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        [HttpPost]
        public ActionResult ViewPDF()
        {
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            TempData["Embed"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/Download/Files/FI_DI_01.pdf"));

            return RedirectToAction("Index");
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

        public ActionResult Organigrama()
        {
            return View();

        }
        public ActionResult Download(int id)
        {
            
            OPEntities contexto = new OPEntities();
            try
            {
                var elemento = contexto.TablaMaestra.Find(id);
                if (elemento != null)
                {
                    DownloaderController x = new DownloaderController();
                    return x.DownloadFile(elemento.Codigo.Trim(), elemento.Link.Trim());
                }
            }
            catch
            {
                return HttpNotFound();
            }
            return  RedirectToAction("index", "home");


            /*/ Obtener contenido del archivo
            string text = "El texto para mi archivo.";
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(text));

            return File(stream, "text/plain", "Prueba.txt");
            */
        }

        public ActionResult Directorio()
        {
            OPEntities contexto = new OPEntities();
            try
            {
                var elemento = contexto.TablaDirectorio.ToList();
                if (elemento != null)
                {

                    return View(elemento);
                } 
            }
            catch
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }


    }
    /*
    public abstract class PdfResult : FileResult
    {
        #region Constructor
        ///
        /// Initializes a new instance of the FileResult class.
        ///

        public PdfResult()
            : base(System.Net.Mime.MediaTypeNames.Application.Pdf)
        {
        }

        #endregion
    }
    public class PdfStreamResult : PdfResult
    {
        // Fields
        private const int _bufferSize = 0x1000;

        #region Constructor

        ///
        /// PdfContentResult
        /// Initializes a new instance of the FileStreamResult class.
        ///
        public PdfStreamResult(Stream fileStream)
            : base()
        {
            if (fileStream == null)
            {
                throw new ArgumentNullException("fileStream");
            }
            this.FileStream = fileStream;
        }

        #endregion

        #region Overriden Methods

        ///
        /// WriteFile
        /// Writes the file to the response. (Overrides FileResult.WriteFile(HttpResponseBase).)
        protected override void WriteFile(HttpResponseBase response)
        {
            Stream outputStream = response.OutputStream;
            using (this.FileStream)
            {
                byte[] buffer = new byte[0x1000];
                while (true)
                {
                    int count = this.FileStream.Read(buffer, 0, 0x1000);
                    if (count == 0)
                    {
                        return;
                    }
                    outputStream.Write(buffer, 0, count);
                }
            }
        }

        #endregion

        ///
        /// FileContents
        /// Gets the stream that will be sent to the response.
        public Stream FileStream { get; private set; }
    }
    public class PdfPathResult : PdfResult
    {
        #region Constructor

        ///
        /// PdfContentResult
        /// Initializes a new instance of the FilePathResult class by using the specified file name and content type.
        ///
        public PdfPathResult(string fileName)
            : base()
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("Value cannot be null or empty.", "fileName");
            }
            this.FileName = fileName;
        }

        #endregion

        #region Overriden Methods

        ///
        /// WriteFile
        /// Writes the file to the response. (Overrides FileResult.WriteFile(HttpResponseBase).)
        protected override void WriteFile(HttpResponseBase response)
        {
            response.TransmitFile(this.FileName);
        }

        #endregion

        ///
        /// FileContents
        /// Gets the path of the file that is sent to the response.
        public string FileName { get; private set; }
    }

    public class PdfContentResult : PdfResult
    {
        #region Constructor

        ///
        /// PdfContentResult
        /// Initializes a new instance of the PdfContentResult class by using the specified file contents and content type.
        ///
        public PdfContentResult(byte[] fileContents)
            : base()
        {
            if (fileContents == null)
            {
                throw new ArgumentNullException("fileContents");
            }
            this.FileContents = fileContents;
        }

        #endregion

        #region Overriden Methods

        ///
        /// WriteFile
        /// Writes the file content to the response. (Overrides FileResult.WriteFile(HttpResponseBase).)
        protected override void WriteFile(HttpResponseBase response)
        {
            response.OutputStream.Write(this.FileContents, 0, this.FileContents.Length);
        }

        #endregion

        ///
        /// FileContents
        /// The binary content to send to the response.
        public byte[] FileContents { get; private set; }
    }
    public static class ControllerExtensions
    {
        #region Pdf Extension method for Controller

        ///
        /// Pdf
        /// Creates a PdfContentResult object by using the file contents, content type, and the destination file name.
        /// The Controller
        /// The binary content to send to the response.
        /// The file name to use in the file-download dialog box that is displayed in the browser.
        /// The file-content result object.
        public static PdfResult Pdf(this Controller controller, byte[] fileContents, string fileDownloadName)
        {
            return new PdfContentResult(fileContents) { FileDownloadName = fileDownloadName };
        }

        ///
        /// Pdf
        /// Creates a PdfStreamResult object using the Stream object, the content type, and the target file name.
        /// The Controller
        /// The stream to send to the response.
        /// The file name to use in the file-download dialog box that is displayed in the browser.
        /// The file-stream result object.
        public static PdfResult Pdf(this Controller controller, Stream fileStream, string fileDownloadName)
        {
            return new PdfStreamResult(fileStream) { FileDownloadName = fileDownloadName };
        }

        ///
        /// Pdf
        /// Creates a PdfPathResult object by using the file name, the content type, and the file download name.
        /// The Controller
        /// The path of the file to send to the response.
        /// The file name to use in the file-download dialog box that is displayed in the browser.
        /// The file-stream result object.
        public static PdfResult Pdf(this Controller controller, string fileName, string fileDownloadName)
        {
            return new PdfPathResult(fileName) { FileDownloadName = fileDownloadName };
        }

        #endregion
    }
    */
}