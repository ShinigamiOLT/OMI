using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Omu.AwesomeMvc;

namespace OMI.Models
{
    public class PedidoInPut
    {
        public int Id { get; set; }
        public int  IdSolicitud { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetUnidad", Controller = "Data")]
        [DisplayName("Unidad") ]
        public int IdUnidad { get; set; }
        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetCategoria", Controller = "Data")]
        [DisplayName("Categoria")]
        public int Categoria { get; set; }

        public PedidoInPut()
        {
            Id = 0;
            IdSolicitud = 1;
            IdUnidad = 1;
            Cantidad = 1;
            Descripcion = "Nuevo";
        }
    }
    public class PedidoPInPut
    {
        public int Id { get; set; }
        public int IdSolicitud { get; set; }
        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetEspecialidad", Controller = "Data")]
        [DisplayName("Tipo Especialidad")]
        public int Especialidad { get; set; }

        [UIHint("Odropdown")]
        [AweUrl(Action = "GetProfesion", Controller = "Data")]
        [DisplayName("Profesion")]
        public int Profesion { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetCategoriaRH", Controller = "Data")]
        [DisplayName("Categoria")]
        public int Categoria { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public PedidoPInPut()
        {
            Id = 0;
            IdSolicitud = 1;
            Categoria = 1;
            Profesion = 1;
            Especialidad = 1;
            Cantidad = 1;
            Descripcion = "";
        }
    }

    public class AutorizaInput
    {
        public int id { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAutorizacion", Controller = "Data")]
        [DisplayName("Solicitud Compra")]
        public int Autorizar { get; set; }

        public String Observacion { get; set; }
    }

    public class ProveedorInput
    {
        public int id { get; set; }
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetCompra", Controller = "Data")]
        [Required]
        [DisplayName("Orden de Compra")]
        public int? Autorizar { get; set; }

        [UIHint("Odropdown")]
        [AweUrl(Action = "GetProveedor", Controller = "Data")]
        [DisplayName("Proveedor")]
        public int Proveedor { get; set; }


        [DisplayName("Otro Provedoor")]
        public string OtroProvedor { get; set; }

        public String Observacion { get; set; }
        public String Nota { get; set; }
    }

    public class PrecioUnitarioInput
    {


       
        [DisplayName("Precio")]
        public decimal Precio { get; set; }

        public PrecioUnitarioInput()
        {
            Precio = 0;
        }

        public int Id { get; set; }
    }
    public class AutocompleteDemoInput
    {
        public string Nombre { get; set; }

        public string ParentCategory { get; set; }
        public int? ParentCategoryId { get; set; }

        public int PrimeNumber { get; set; }

        public string ChildMeal { get; set; }

        public int[] CategoriesData { get; set; }

        public int? CategoryData { get; set; }

        public string ChildOfManyMeal { get; set; }

        public string Meal1 { get; set; }
        public string Meal2 { get; set; }
    }

}