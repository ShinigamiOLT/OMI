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
    
    public partial class TbPedidoPersonal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TbPedidoPersonal()
        {
            this.TbSolicitud = new HashSet<TbSolicitud>();
        }
    
        public int Id { get; set; }
        public Nullable<int> IdTipoEspecialidad { get; set; }
        public Nullable<int> IdProfesion { get; set; }
        public Nullable<int> IdCategoriaRH { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<int> Supervisor { get; set; }
        public Nullable<int> Estatus { get; set; }
    
        public virtual TbCategoriaRH TbCategoriaRH { get; set; }
        public virtual TbEspecialidad TbEspecialidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TbSolicitud> TbSolicitud { get; set; }
        public virtual TbProfesion TbProfesion { get; set; }
    }
}
