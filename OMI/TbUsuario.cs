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
    
    public partial class TbUsuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TbUsuario()
        {
            this.Supervisores = new HashSet<Supervisores>();
            this.TbPedidoPersonal = new HashSet<TbPedidoPersonal>();
            this.TbSolicitud = new HashSet<TbSolicitud>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Area { get; set; }
    
        public virtual TbArea TbArea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supervisores> Supervisores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TbPedidoPersonal> TbPedidoPersonal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TbSolicitud> TbSolicitud { get; set; }
    }
}
