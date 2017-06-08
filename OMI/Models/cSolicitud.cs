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
        

        //aqui se supone que sera para dar de alta una solicitud.
        public  OPEntities contexto;
     

        public string Folio;
       

        public TbSolicitud TbSol;

        public List<TbPedidoM> ListaPedido;

        public bool Guardar;
     
        public cSolicitud()
        {
            int   idformato = 1;
           int  IdSolicitud = 2;
            int idusuario = 2;
            contexto= new OPEntities();
            TbSol = contexto.TbSolicitud.Find(IdSolicitud);
            if (TbSol == null)
            {
                TbSol= new TbSolicitud();
                TbSol.Fecha = DateTime.Now;
                TbSol.IdFormato = idformato;
                TbSol.IdUsuario = idusuario;
                TbSol.TipoMaterialPersonal = 1;
                
                contexto.TbSolicitud.Add(TbSol);
                contexto.SaveChanges();

            }

          TbSol.TbFormato   = contexto.TbFormato.Find(idformato);

            TbSol.TbUsuario = contexto.TbUsuario.Find(idusuario);



            ListaPedido = TbSol.TbPedidoM;
            Folio =  TbSol.TbFormato.Nombre+"-" + IdSolicitud.ToString("000");
            
        }


       

        public string FechaCorta {
            get { return TbSol. Fecha.ToShortDateString(); }
        }

        public  TbPedidoM Get<T> (int id,int id2) where T : TbPedidoM
        {

            return  contexto.TbPedidoM.Find(id,id2);
        }

       
        public TbCategoria GetCategoria(int? id)
        {

            return contexto.TbCategoria.Find(id);
        }

        internal void GuardaPedidos()
        {
            foreach (TbPedidoM m in ListaPedido)
            {
                if (m.Dato != 2)
                    m.Dato = 1;

            }
            EliminaPedidos(2);
            contexto.SaveChanges();
        }

        internal void EliminaPedidos(int clave)
        {
            foreach (TbPedidoM m in ListaPedido.ToList())
            {
                
                if (m.Dato == clave)
                {
                    contexto.Entry(m).State= EntityState.Deleted;
                }
               

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

        internal TbPedidoM GetPedidoM(int id,int id2)
        {
           
            return contexto.TbPedidoM.Find(id,id2);
        }

        public void UpdatePedido(PedidoInPut input)
        {
            var dinner = GetPedidoM(input.Id,input.IdSolicitud);

            dinner.Cantidad = input.Cantidad;
            dinner.IdUnidad = input.IdUnidad;
            dinner.Descripcion = input.Descripcion;
            dinner.IdCategoria = input.Categoria;
            contexto.Entry(dinner).State = EntityState.Modified;
            contexto.SaveChanges();

        }

        public void DelPedido(int inputId)
        {
           contexto.Entry(GetPedidoM(inputId,TbSol.IdSolicitud)).State = EntityState.Deleted;
            contexto.SaveChanges();
        }
        public void DelPedido2(int inputId)
        {
       //     contexto.Entry(GetPedidoM(inputId, TbSol.IdSolicitud)).State = EntityState.Deleted;
            GetPedidoM(inputId, TbSol.IdSolicitud).Dato = 2;
            contexto.SaveChanges();
        }

        public void DesMarcaEliminado(int clave)
        {
            foreach (TbPedidoM m in ListaPedido.ToList())
            {
                
                if (m.Dato == clave)
                {
                    m.Dato = 1;
                }


            }
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