using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OMI.Models
{
    public class cSolicitud
    {
         [Key]
         public int IdSolicitud;

        public string Folio;

        public DateTime Fecha;

        //aqui se supone que sera para dar de alta una solicitud.
        public  OPEntities contexto;
        private int idformato;
        public TbFormato tipoformato;

        public TbUsuario usuario;

        public List<TbPedidoM> ListaPedido;
     
        public cSolicitud()
        {
            idformato = 1;
            IdSolicitud = 2;
            contexto= new OPEntities();

            tipoformato = contexto.TbFormato.Find(idformato);

            usuario = contexto.TbUsuario.Find(2);

            ListaPedido = new List<TbPedidoM>();

          

            Folio = tipoformato.Nombre+"-" + IdSolicitud.ToString("000");
            Fecha = DateTime.Now;
        }


        public string Datos(int id)
        {
            return usuario.Nombre + " : " + usuario.TbArea.Nombre + ": " + tipoformato.Nombre + " : " + tipoformato.Descripcion + id;
        }

        public string FechaCorta {
            get { return Fecha.ToShortDateString(); }
        }

        public  TbPedidoM Get<T> (int id) where T : TbPedidoM
        {

            return  contexto.TbPedidoM.Find(id);
        }

       
        public TbCategoria GetCategoria(int? id)
        {

            return contexto.TbCategoria.Find(id);
        }

        internal void GuardaPedidos()
        {
            foreach (TbPedidoM m in ListaPedido)
            {
                contexto.TbPedidoM.Add(m);

            }
            contexto.SaveChanges();
        }

        internal int GeneraIdPedidoM()
        {
            int siguiente = 0;
            var elemento = contexto.TbPedidoM.Max(x => x.Id);
            if (elemento != null)
            {
                siguiente = elemento + 1;
            }
            return siguiente;
        }

        public void AgregaPedido(TbPedidoM dinner)
        {
            dinner.Id = GeneraIdPedidoM();
        contexto.TbPedidoM.Add(dinner);
            contexto.SaveChanges();
        }

        internal TbPedidoM GetPedidoM(int id)
        {
            return contexto.TbPedidoM.Find(id);
        }

        public void UpdatePedido(PedidoInPut input)
        {
            var dinner = GetPedidoM(input.Id);

            dinner.Cantidad = input.Cantidad;
            dinner.IdUnidad = input.IdUnidad;
            dinner.Descripcion = input.Descripcion;
            dinner.IdCategoria = input.Categoria;
            contexto.Entry(dinner).State = EntityState.Modified;
            contexto.SaveChanges();

        }

        public void DelPedido(int inputId)
        {
           contexto.Entry(GetPedidoM(inputId)).State = EntityState.Deleted;
            contexto.SaveChanges();
        }
    }

    public class DeleteConfirmInput
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string GridId { get; set; }
    }
}