//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class TbInve_Equipo_Comp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TbInve_Equipo_Comp()
        {
            this.TbInve_Licencias = new HashSet<TbInve_Licencias>();
        }
    
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Serie { get; set; }
        public string Procesador { get; set; }
        public string Accesorio { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> FechaAlta { get; set; }
        public Nullable<System.DateTime> FechaMantenimiento { get; set; }
        public Nullable<int> Estado { get; set; }
        public string Descripcion { get; set; }
        public string NumeroOne { get; set; }
        public string Caracteristicas { get; set; }
        public string Modelo { get; set; }
        public bool Visible { get; set; }
    
        public virtual TbUsuario TbUsuario { get; set; }
        public virtual Tb_EstadoEquipo Tb_EstadoEquipo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TbInve_Licencias> TbInve_Licencias { get; set; }
    }
}
