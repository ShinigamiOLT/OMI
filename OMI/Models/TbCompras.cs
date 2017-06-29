using System;
using OIM_DAL;

namespace OMI.Models
{
    public class TbComprasw
    {
        public DateTime FechaOrden { get; set; }
        public int Id { get; set; }
        public int IdFormato { get; set; }
        public int IdUsuario { get; set; }
        public int IdProveedor { get; set; }
        public TbUsuario TbUsuario { get; set; }
        public TbFormato TbFormato { get; set; }
        public TbProveedores TbProveedores { get; set; }
        public string Folio { get; set; }
    }
}