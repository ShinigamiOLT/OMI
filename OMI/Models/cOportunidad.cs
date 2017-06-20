using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMI.Models
{
    public class cOportunidad
    {
        public TbOportunidad oportunidad;
        public List<TbAreaInteres> LAreas;
        public List<TbTipoOportunidad> LOportunidad;

        public cOportunidad()
        {
            OPEntities contexto= new OPEntities();
         oportunidad = new TbOportunidad();
            LAreas = contexto.TbAreaInteres.ToList();
            LOportunidad = contexto.TbTipoOportunidad.ToList();
           

        }
    }
}
