using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMI.Models
{
    public class UtilApp
    {
       public OPEntities entidad;

        public UtilApp()
        {
            entidad = new OPEntities();
        }

        public List<TbArea> GetAreas()
        {

            return entidad.TbArea.ToList();
        }

        public TbArea GetArea(int id)
        {

            return entidad.TbArea.SingleOrDefault(x => x.Id == id);
        }


        public void Insert(TbArea Area)
        {
            entidad.TbArea.Add(Area);
            entidad.SaveChanges();
        }

        public void Update(TbArea Area)
        {

            entidad.Entry(Area).State = EntityState.Modified;

            entidad.SaveChanges();
        }

    }
}