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
    
    public partial class TbInve_Licencias
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Software { get; set; }
        public Nullable<System.DateTime> FCompra { get; set; }
        public Nullable<System.DateTime> FInstalacion { get; set; }
        public Nullable<System.DateTime> FCaducidad { get; set; }
        public string CuentaVinculada { get; set; }
        public string Version { get; set; }
        public Nullable<int> Estado { get; set; }
        public string Observacion { get; set; }
        public Nullable<int> IdEquipoUso { get; set; }
    
        public virtual Tb_EstadoLic Tb_EstadoLic { get; set; }
        public virtual TbInve_Equipo_Comp TbInve_Equipo_Comp { get; set; }
    }
}
