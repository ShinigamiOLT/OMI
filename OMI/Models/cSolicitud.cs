using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OMI.Models
{
    public class cSolicitud
    {
         [Key]
        public int Id;

        public string Folio;

        public DateTime Fecha;
        //aqui se supone que sera para dar de alta una solicitud.
       private  OPEntities contexto;
        private int idformato;
        public TbFormato tipoformato;

        public TbUsuario usuario;

        public List<TbPedidoM> ListaPedido;
     
        public cSolicitud()
        {
            idformato = 2;
            contexto= new OPEntities();

            tipoformato = contexto.TbFormato.Find(idformato);

            usuario = contexto.TbUsuario.Find(2);

            ListaPedido = new List<TbPedidoM>();

            Id = 1;

            Folio = tipoformato.Nombre+"-" + Id.ToString("000");
            Fecha = DateTime.Now;
        }


        public string Datos(int id)
        {
            return usuario.Nombre + " : " + usuario.TbArea.Nombre + ": " + tipoformato.Nombre + " : " + tipoformato.Descripcion + id;
        }

        public string FechaCorta {
            get { return Fecha.ToShortDateString(); }
        }
    }
}