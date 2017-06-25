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
        [DisplayName("Autorizar")]
        public int Autorizar { get; set; }
    }


}