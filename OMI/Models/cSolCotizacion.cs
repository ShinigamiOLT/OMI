using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OIM_DAL;
using OMI.Models.Utils;

namespace OMI.Models
{
    public class cSolCotizacion
    {
        public string FechaCorta
        {
            get
            {
                return TbSolicitud.FechaSol.ToShortDateString();
                ;
            }
        }

        public int Id { get; set; }

        public TbSolicitudCotizacion TbSolicitud { get; set; }

        public OIMEntity contexto;

        public cSolCotizacion()
        {

            contexto = new OIMEntity();
            formadepago = new List<string>();
            datosfacturacion = new List<string>();
            observaciones = new List<string>();
            Id = 0;
            Valores();
        }




        void Valores()
        {
            formadepago.Clear();
            datosfacturacion.Clear();
            observaciones.Clear();
            formadepago.Add("FORMATO DE PAGO: ");
            formadepago.Add(
                "15 % del monto total del contrato al registrar la Orden de Compra.75 % en Erogaciones parciales mensuales de acuerdo a avance logrado. 10 % A la implementación del Sistema SGI Auditado y realizado las acciones de mejora.");

            datosfacturacion.Add("DATOS DE FACTURACIÓN: ");
            datosfacturacion.Add(
                "INGENIERÍA APLICADA ONEPRO S.A.DE C.V. RFC: IAO090907K21 Prolongación Paseo Usumacinta B3 Depto. 3, Col.Conjunto Habitacional Usumacinta, Villahermosa, Tabasco, C.P.86035");

            observaciones.Add("OBSERVACIONES: ");
            observaciones.Add(
                "Esta Orden se realizara de acuerdo al presupuesto presentado BPI-SGI - 16 - 38 por el proveedor y el cronograma Anexo.");




        }

        public void nuevo(int idFormato, int IdPedido)
        {
            if (Id != 0)
            {

                TbSolicitud = contexto.TbSolicitudCotizacion.Find(Id);

            }
            if (TbSolicitud == null)
            {
                TbSolicitud = new TbSolicitudCotizacion();
                Id = (contexto.TbSolicitudCotizacion.Any()) ? contexto.TbSolicitudCotizacion.Max(x => x.Id) + 1 : 1;

                TbSolicitud.Id = Id;
                TbSolicitud.FechaSol = DateTime.Now;
                TbSolicitud.IdFormato = idFormato;
                TbSolicitud.IdUsuario = 2;
                TbSolicitud.IdProveedor = 1;
                TbSolicitud.TbUsuario = contexto.TbUsuario.Find(TbSolicitud.IdUsuario);
                TbSolicitud.TbFormato = contexto.TbFormato.Find(TbSolicitud.IdFormato);
                TbSolicitud.TbProveedores = contexto.TbProveedores.Find(TbSolicitud.IdProveedor);
                TbSolicitud.Folio = TbSolicitud.TbFormato!=null ? TbSolicitud.TbFormato.Nombre + "-" + Id.ToString("000"):"Sin Folio";
                TbSolicitud.IdPedido = IdPedido;
                TbSolicitud.TbPedidoM = contexto.TbPedidoM.Find(IdPedido);
                contexto.TbSolicitudCotizacion.Add(TbSolicitud);
                contexto.SaveChanges();
                TbSolicitud = contexto.TbSolicitudCotizacion.Find(Id);

            }
            Valores();
        }

        public List<string> formadepago { get; set; }
        public List<string> datosfacturacion { get; set; }
        public List<string> observaciones { get; set; }

        
        public TbPedidoM GetPedidoM(int id)
        {
            return contexto.TbPedidoM.Find(id);
        }

        public void SalvarDatos()
        {
            //aqui guardemos los datos que son validos

            TbSolicitud = contexto.TbSolicitudCotizacion.Find(Id);

            TbSolicitud.FechaSol = DateTime.Now;

           

            contexto.SaveChanges();

        }

        public bool EstadoEnvio()
        {
            if (TbSolicitud == null)
                TbSolicitud = contexto.TbSolicitudCotizacion.Find(Id);


           

            return TbSolicitud.TbPedidoM.ConfirmaOrden.HasValue && ( TbSolicitud.TbPedidoM.ConfirmaOrden.Value == 1? true:false);

        }

      
    }
}