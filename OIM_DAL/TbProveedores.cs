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
    
    public partial class TbProveedores
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string RazonSocial { get; set; }
        public string Tipo { get; set; }
        public string Vendedor { get; set; }
        public string Area_Sector_Negocio { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Categoria { get; set; }
        public Nullable<int> Confiable { get; set; }
    }
}