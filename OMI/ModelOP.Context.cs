﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OMI
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class OPEntities : DbContext
    {
        public OPEntities()
            : base("name=OPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TbArea> TbArea { get; set; }
        public virtual DbSet<TbCategoria> TbCategoria { get; set; }
        public virtual DbSet<TbCategoriaRH> TbCategoriaRH { get; set; }
        public virtual DbSet<TbEspecialidad> TbEspecialidad { get; set; }
        public virtual DbSet<TbFormato> TbFormato { get; set; }
        public virtual DbSet<TbPedidoPersonal> TbPedidoPersonal { get; set; }
        public virtual DbSet<TbProfesion> TbProfesion { get; set; }
        public virtual DbSet<TbUnidad> TbUnidad { get; set; }
        public virtual DbSet<TbUsuario> TbUsuario { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TbStatusAutorizacion> TbStatusAutorizacion { get; set; }
        public virtual DbSet<Supervisores> Supervisores { get; set; }
        public virtual DbSet<TbPedidoM> TbPedidoM { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<TbSolicitud> TbSolicitud { get; set; }
    
        public virtual ObjectResult<PedidoMXSolicitud_Result> PedidoMXSolicitud(Nullable<int> idSol)
        {
            var idSolParameter = idSol.HasValue ?
                new ObjectParameter("IdSol", idSol) :
                new ObjectParameter("IdSol", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PedidoMXSolicitud_Result>("PedidoMXSolicitud", idSolParameter);
        }
    
        public virtual ObjectResult<Sp_AllPedido_Result1> Sp_AllPedido()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_AllPedido_Result1>("Sp_AllPedido");
        }
    }
}
