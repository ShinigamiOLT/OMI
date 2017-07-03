using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OIM_DAL;

namespace OMI.Models
{
    public class cSolicitud
    {
         [Key]
        

        //aqui se supone que sera para dar de alta una solicitud.
        public  OIMEntity contexto;
     
        public TbSolicitud TbSol;
        public bool Valido;

        public cSolicitud(int idSol,int idformato)
        {
            Valido = false;
            int idusuario = 2;
            contexto = new OIMEntity();
            TbSol = contexto.TbSolicitud.Find(idSol);
            if (TbSol == null)
            {
                CreaNuevaSolicitud(idformato, idusuario);
            }
            //actualizamos el nombre del formato
            TbSol.TbFormato = contexto.TbFormato.Find(idformato);
            TbSol.Folio = TbSol.TbFormato.Nombre + "-" + TbSol.IdSolicitud.ToString("000");
            contexto.SaveChanges();

            if (TbSol.IdFormato == idformato)
                Valido = true;
        }
        void CreaNuevaSolicitud(int idformato, int idusuario)
        {
            TbSol = new TbSolicitud();
            TbSol.Fecha = DateTime.Now;
            TbSol.IdFormato = idformato;
            TbSol.IdUsuario = idusuario;
            TbSol.TipoMaterialPersonal = idformato;
            contexto.TbSolicitud.Add(TbSol);
            contexto.SaveChanges();
            TbSol.TbFormato = contexto.TbFormato.Find(idformato);
            TbSol.TbUsuario = contexto.TbUsuario.Find(idusuario);
            TbSol.Folio = TbSol.TbFormato.Nombre + "-" + TbSol.IdSolicitud.ToString("000");
            contexto.SaveChanges();
        }
        
        public string FechaCorta {
            get { return TbSol. Fecha.ToShortDateString(); }
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
            
            foreach (TbPedidoM m in TbSol.TbPedidoM.ToList())
            {
                if (m.Dato != 2)
                    m.Dato = 1;

            }
            EliminaPedidos(2);
            contexto.SaveChanges();
        }

        internal void EliminaPedidos(int clave)
        {
            foreach (TbPedidoM m in TbSol.TbPedidoM.ToList())
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

            if (contexto.TbPedidoM.Count()>0)
            {

siguiente= contexto.TbPedidoM.Max(x => x.Id);
            } 
            return (siguiente +1);
        }

        internal int GeneraIdPedidoP()
        {
            int siguiente = 0;
            var elemento = contexto.TbPedidoPersonal.Max(x => x.Id);
            if (elemento != null)
            {
                siguiente = elemento + 1;
            }
            return siguiente;
        }

        public void AgregaPedido(TbPedidoM dinner)
        {
            dinner.Id = GeneraIdPedidoM();
            dinner.OrdenCompra = null;
            dinner.TbOrdenCompra = contexto.TbOrdenCompra.Find(1);
        contexto.TbPedidoM.Add(dinner);
            contexto.SaveChanges();
        }


        internal TbPedidoM GetPedidoM(int id_P)
        {
           
            return contexto.TbPedidoM.Find(id_P);
        }
        internal TbPedidoPersonal GetPedidoP(int id)
        {

            return contexto.TbPedidoPersonal.Find(id);
        }
        public void UpdatePedido(PedidoInPut input)
        {
            var dinner = GetPedidoM(input.Id);

            dinner.Cantidad = input.Cantidad;
            dinner.IdUnidad = input.IdUnidad;
            dinner.Descripcion = input.Descripcion;
            dinner.IdCategoria = input.Categoria;
            dinner.OrdenCompra = 4;
            contexto.Entry(dinner).State = EntityState.Modified;
            contexto.SaveChanges();

        }
        public void UpdatePedido(AutorizaInput input)
        {
            var dinner = GetPedidoM(input.id);

            dinner.Estatus = input.Autorizar;
            dinner.Observacion = input.Observacion;
            dinner.FechaAutorizacion = DateTime.Now.Date;
            contexto.Entry(dinner).State = EntityState.Modified;
            contexto.SaveChanges();

        }
        public void UpdatePedido(ProveedorInput input)
        {
            var dinner = GetPedidoM(input.id);
            dinner.IdProveedor = input.Proveedor;
            if (input.Proveedor == 0)
            {
                dinner.Proveedor = input.OtroProvedor;
               
            }
            else
            {


                dinner.Proveedor = contexto.TbProveedores.Find(input.Proveedor).Nombre;
            }

            dinner.OrdenCompra = input.Autorizar;

            dinner.Observacion = input.Observacion;
            dinner.FechaAutorizacion = DateTime.Now.Date;
            contexto.Entry(dinner).State = EntityState.Modified;
            contexto.SaveChanges();

        }
        public void UpdatePedidoP(PedidoPInPut input)
        {
            var dinner = GetPedidoP(input.Id);

            dinner.Cantidad = input.Cantidad;
            dinner.IdTipoEspecialidad = input.Especialidad;
            dinner.IdProfesion = input.Profesion;
            dinner.IdCategoriaRH = input.Categoria;
            dinner.Descripcion = input.Descripcion;
            contexto.Entry(dinner).State = EntityState.Modified;
            contexto.SaveChanges();

        }

        public void DelPedido(int inputId)
        {
           contexto.Entry(GetPedidoM(inputId)).State = EntityState.Deleted;
            contexto.SaveChanges();
        }
        public void DelPedidoM(int inputId)
        {
       //     contexto.Entry(GetPedidoM(inputId, TbSol.IdSolicitud)).State = EntityState.Deleted;
            GetPedidoM(inputId).Dato = 2;
            contexto.SaveChanges();
        }
        public void DelPedidoP(int inputId)
        {
            //     contexto.Entry(GetPedidoM(inputId, TbSol.IdSolicitud)).State = EntityState.Deleted;
            GetPedidoP(inputId).Dato = 2;
            contexto.SaveChanges();
        }

        public void DesMarcaEliminado(int clave)
        {
            foreach (TbPedidoM m in TbSol.TbPedidoM.ToList())
            {
                
                if (m.Dato == clave)
                {
                    m.Dato = 1;
                }


            }
            contexto.SaveChanges();
        }

        public void EnviarPedido()
        {
            TbSol.EnviadoInfra = 1;
            TbSol.EnviadoCom = 0;
           
            contexto.SaveChanges();
        }
        public void EnviarPedidoCom()
        {
            TbSol.EnviadoCom = 1;
           
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