using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OIM_DAL;

namespace OMI.Models
{
    public class COrdenCompra
    {
        public string FechaCorta {
            get { return TbCompras_.FechaOrden.ToShortDateString(); ;
        }
        }

        public TbCompras TbCompras_ { get; set; }

        private OIMEntity contexto;

        public COrdenCompra()
        {

            contexto = new OIMEntity();
            formadepago= new List<string>();
            datosfacturacion = new List<string>();
            observaciones = new List<string>();

            formadepago.Add( "FORMATO DE PAGO: ");
            formadepago.Add("15 % del monto total del contrato al registrar la Orden de Compra.75 % en Erogaciones parciales mensuales de acuerdo a avance logrado. 10 % A la implementación del Sistema SGI Auditado y realizado las acciones de mejora.");

           datosfacturacion.Add("DATOS DE FACTURACIÓN: ");
            datosfacturacion.Add("INGENIERÍA APLICADA ONEPRO S.A.DE C.V. RFC: IAO090907K21 Prolongación Paseo Usumacinta B3 Depto. 3, Col.Conjunto Habitacional Usumacinta, Villahermosa, Tabasco, C.P.86035");

            observaciones.Add("OBSERVACIONES: ");
            observaciones.Add("Esta Orden se realizara de acuerdo al presupuesto presentado BPI-SGI - 16 - 38 por el proveedor y el cronograma Anexo.");



        }

        public void nuevo ()
        { 
        TbCompras_ = new TbCompras();

            int id=( contexto.TbCompras.Any()) ? contexto.TbCompras.Max(x=> x.Id) +1 : 1;
            TbCompras_.Id = id;
            TbCompras_.FechaOrden = DateTime.Now;
            TbCompras_.IdFormato = 6;
            TbCompras_.IdUsuario = 2;
            TbCompras_.IdProveedor = 1;
            TbCompras_.TbUsuario = contexto.TbUsuario.Find(TbCompras_.IdUsuario);
            TbCompras_.TbFormato = contexto.TbFormato.Find(TbCompras_.IdFormato);
            TbCompras_.TbProveedores = contexto.TbProveedores.Find(TbCompras_.IdProveedor);
            TbCompras_.Folio = TbCompras_.TbFormato.Nombre + "-"+id.ToString("000");

            contexto.TbCompras.Add(TbCompras_);
            contexto.SaveChanges();
            TbCompras_ = contexto.TbCompras.Find(id);

            
        }

        public List<string> formadepago  { get; set; }
        public List<string> datosfacturacion { get; set; }
        public List<string> observaciones { get; set; }

    }
}