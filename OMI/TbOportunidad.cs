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
    
    public partial class TbOportunidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TbOportunidad()
        {
            this.TbAreaInteres = new List<TbAreaInteres>();
            this.TbTipoOportunidad = new HashSet<TbTipoOportunidad>();
        }
    
        public int Id { get; set; }
        public string Folio { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.DateTime FechaSistema { get; set; }
        public string Representante { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int MedioContacto { get; set; }
        public string Descripcion { get; set; }
        public int idFormato { get; set; }
        public int idUsuario { get; set; }
        public string Compania { get; set; }
    
        public virtual TbFormato TbFormato { get; set; }
        public virtual TbMedioContacto TbMedioContacto { get; set; }
        public virtual TbUsuario TbUsuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<TbAreaInteres> TbAreaInteres { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TbTipoOportunidad> TbTipoOportunidad { get; set; }
    }
}