//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class TbPedidoM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TbPedidoM()
        {
            this.TbSolicitud = new HashSet<TbSolicitud>();
        }
    
        public int Id { get; set; }
        public Nullable<int> IdSolicitud { get; set; }
        public Nullable<int> IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> IdUnidad { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<int> IdSupervisor { get; set; }
        public Nullable<int> Estatus { get; set; }
        public Nullable<int> Surtido { get; set; }
        public Nullable<int> Dato { get; set; }
    
        public virtual TbCategoria TbCategoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TbSolicitud> TbSolicitud { get; set; }
        public virtual TbUnidad TbUnidad { get; set; }
        public virtual TbStatusAutorizacion TbStatusAutorizacion { get; set; }
        public virtual Supervisores Supervisores { get; set; }
    }
}
