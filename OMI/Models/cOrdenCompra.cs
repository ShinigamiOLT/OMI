﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OIM_DAL;
using OMI.Models.Utils;

namespace OMI.Models
{
    public class COrdenCompra
    {
        public string FechaCorta
        {
            get
            {
                return TbCompras_.FechaOrden.ToShortDateString();
                ;
            }
        }

        public int Id { get; set; }

        public TbCompras TbCompras_ { get; set; }

        public OIMEntity contexto;

        public COrdenCompra()
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

        public void nuevo(int idFormato)
        {
            if (Id != 0)
            {

                TbCompras_ = contexto.TbCompras.Find(Id);

            }
            if (TbCompras_ == null)
            {
                TbCompras_ = new TbCompras();
                Id = (contexto.TbCompras.Any()) ? contexto.TbCompras.Max(x => x.Id) + 1 : 1;

                TbCompras_.Id = Id;
                TbCompras_.FechaOrden = DateTime.Now;
                TbCompras_.IdFormato = idFormato;
                TbCompras_.IdUsuario = 2;
                TbCompras_.IdProveedor = 1;
                TbCompras_.TbUsuario = contexto.TbUsuario.Find(TbCompras_.IdUsuario);
                TbCompras_.TbFormato = contexto.TbFormato.Find(TbCompras_.IdFormato);
                TbCompras_.TbProveedores = contexto.TbProveedores.Find(TbCompras_.IdProveedor);
                TbCompras_.Folio = TbCompras_.TbFormato.Nombre + "-" + Id.ToString("000");

                contexto.TbCompras.Add(TbCompras_);
                contexto.SaveChanges();
                TbCompras_ = contexto.TbCompras.Find(Id);

            }
            Valores();
        }

        public List<string> formadepago { get; set; }
        public List<string> datosfacturacion { get; set; }
        public List<string> observaciones { get; set; }

        public void ListaPedidos(int[] reglon)
        {
            //de la lista de pedidos asociamos a los numeros de orden de compra de aqui
            TbPedidoM elemento = null;
            foreach (var i in reglon)
            {
                elemento = contexto.TbPedidoM.Find(i);
                if (elemento != null)
                {

                    elemento.IdOrdenCompra = TbCompras_.Id;
                    contexto.Entry(elemento).State = EntityState.Modified;
                }
            }

            if (elemento != null)
            {
                //sacamos el primer dato.
                if (elemento.IdProveedor != 0)
                {


                    TbCompras_.IdProveedor = elemento.IdProveedor;
                    TbCompras_.TbProveedores = contexto.TbProveedores.Find(elemento.IdProveedor);
                }

            }

            contexto.SaveChanges();
        }

        public TbPedidoM GetPedidoM(int id)
        {
            return contexto.TbPedidoM.Find(id);
        }

        public void SalvarDatos()
        {
            //aqui guardemos los datos que son validos

            TbCompras_ = contexto.TbCompras.Find(Id);

            TbCompras_.FechaOrden = DateTime.Now;
            
            foreach (TbPedidoM tbPedidoM in TbCompras_.TbPedidoM)
            {
                tbPedidoM.ConfirmaOrden = (int) Confirma.Si;
            }

            contexto.SaveChanges();

        }

        public  bool EstadoEnvio()
        {
            if(TbCompras_ == null)
                TbCompras_ = contexto.TbCompras.Find(Id);


            foreach (TbPedidoM tbPedidoM in TbCompras_.TbPedidoM)
            {
                return tbPedidoM.ConfirmaOrden != 1;
            }

            return false;

        }

        public void EliminaOrden()
        {
            if (TbCompras_ == null)
                TbCompras_ = contexto.TbCompras.Find(Id);


            foreach (TbPedidoM tbPedidoM in TbCompras_.TbPedidoM)
            {
                tbPedidoM.IdOrdenCompra = null;
                 tbPedidoM.ConfirmaOrden =(int) Confirma.No;
            }
        }
    }
}