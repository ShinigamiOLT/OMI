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

        [UIHint("Lookup")]
        [Required]
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
}