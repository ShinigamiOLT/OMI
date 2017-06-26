using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OIM_DAL;

namespace OMI.Models
{
    public class cOportunidad
    {
        public TbOportunidad oportunidad;
        public List<TbAreaInteres> LAreas;
        public List<TbTipoOportunidad> LOportunidad;

        public cOportunidad()
        {
            OIMEntity contexto= new OIMEntity();
         oportunidad = new TbOportunidad();
            LAreas = contexto.TbAreaInteres.ToList();
            LOportunidad = contexto.TbTipoOportunidad.ToList();
           

        }
    }
}
