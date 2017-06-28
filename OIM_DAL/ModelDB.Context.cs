﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OIM_DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class OIMEntity : DbContext
    {
        public OIMEntity()
            : base("name=OIMEntity")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Supervisores> Supervisores { get; set; }
        public virtual DbSet<TablaDirectorio> TablaDirectorio { get; set; }
        public virtual DbSet<TablaMaestra> TablaMaestra { get; set; }
        public virtual DbSet<TbArea> TbArea { get; set; }
        public virtual DbSet<TbAreaInteres> TbAreaInteres { get; set; }
        public virtual DbSet<TbCategoria> TbCategoria { get; set; }
        public virtual DbSet<TbCategoriaRH> TbCategoriaRH { get; set; }
        public virtual DbSet<TbEspecialidad> TbEspecialidad { get; set; }
        public virtual DbSet<TbFormato> TbFormato { get; set; }
        public virtual DbSet<TbMedioContacto> TbMedioContacto { get; set; }
        public virtual DbSet<TbOportunidad> TbOportunidad { get; set; }
        public virtual DbSet<TbPedidoM> TbPedidoM { get; set; }
        public virtual DbSet<TbPedidoPersonal> TbPedidoPersonal { get; set; }
        public virtual DbSet<TbProfesion> TbProfesion { get; set; }
        public virtual DbSet<TbSolicitud> TbSolicitud { get; set; }
        public virtual DbSet<TbStatusAutorizacion> TbStatusAutorizacion { get; set; }
        public virtual DbSet<TbTipoOportunidad> TbTipoOportunidad { get; set; }
        public virtual DbSet<TbUnidad> TbUnidad { get; set; }
        public virtual DbSet<TbUnidadNegocios> TbUnidadNegocios { get; set; }
        public virtual DbSet<TbUnidadTecnica> TbUnidadTecnica { get; set; }
        public virtual DbSet<TbUsuario> TbUsuario { get; set; }
        public virtual DbSet<TbProveedores> TbProveedores { get; set; }
        public virtual DbSet<TbOrdenCompra> TbOrdenCompra { get; set; }
        public virtual DbSet<TbCompras> TbCompras { get; set; }
    
        public virtual ObjectResult<PedidoMXSolicitud_Result> PedidoMXSolicitud(Nullable<int> idSol)
        {
            var idSolParameter = idSol.HasValue ?
                new ObjectParameter("IdSol", idSol) :
                new ObjectParameter("IdSol", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PedidoMXSolicitud_Result>("PedidoMXSolicitud", idSolParameter);
        }
    
        public virtual ObjectResult<PedidosXFolioXEstatus_Result> PedidosXFolioXEstatus(Nullable<int> idSol, Nullable<int> estatus)
        {
            var idSolParameter = idSol.HasValue ?
                new ObjectParameter("IdSol", idSol) :
                new ObjectParameter("IdSol", typeof(int));
    
            var estatusParameter = estatus.HasValue ?
                new ObjectParameter("Estatus", estatus) :
                new ObjectParameter("Estatus", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PedidosXFolioXEstatus_Result>("PedidosXFolioXEstatus", idSolParameter, estatusParameter);
        }
    
        public virtual ObjectResult<Sp_AllPedido_Result> Sp_AllPedido()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_AllPedido_Result>("Sp_AllPedido");
        }
    
        public virtual ObjectResult<Sp_AllPedidoXEstatus_Result2> Sp_AllPedidoXEstatus(Nullable<int> estatus)
        {
            var estatusParameter = estatus.HasValue ?
                new ObjectParameter("Estatus", estatus) :
                new ObjectParameter("Estatus", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_AllPedidoXEstatus_Result2>("Sp_AllPedidoXEstatus", estatusParameter);
        }
    
        public virtual ObjectResult<Sp_AllPedidoXEstatusXAdmin_Result> Sp_AllPedidoXEstatusXAdmin(Nullable<int> estatus, Nullable<int> orden)
        {
            var estatusParameter = estatus.HasValue ?
                new ObjectParameter("Estatus", estatus) :
                new ObjectParameter("Estatus", typeof(int));
    
            var ordenParameter = orden.HasValue ?
                new ObjectParameter("Orden", orden) :
                new ObjectParameter("Orden", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_AllPedidoXEstatusXAdmin_Result>("Sp_AllPedidoXEstatusXAdmin", estatusParameter, ordenParameter);
        }
    }
}
