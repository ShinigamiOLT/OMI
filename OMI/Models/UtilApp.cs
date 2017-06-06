using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMI.Models
{
    public class UtilApp
    {
        OPEntities entidad;
        public UtilApp()
        {
            entidad = new OPEntities();
        }

        public List<TbArea> GetAreas()
        {

            return entidad.TbArea.ToList();
        }

    }

}