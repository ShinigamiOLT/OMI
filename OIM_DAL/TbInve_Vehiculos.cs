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
    
    public partial class TbInve_Vehiculos
    {
        public int Id { get; set; }
        public string Placas { get; set; }
        public string NumeroOne { get; set; }
        public string Tipo { get; set; }
        public Nullable<System.DateTime> FechaAlta { get; set; }
        public string Factura { get; set; }
        public string Arrendadora { get; set; }
        public string Descripcion { get; set; }
        public string Serie { get; set; }
        public string RentaMensual { get; set; }
        public bool Visible { get; set; }
        public string Modelo { get; set; }
        public Nullable<int> Estatus { get; set; }
    
        public virtual Tb_EstadoVehiculo Tb_EstadoVehiculo { get; set; }
    }
}
