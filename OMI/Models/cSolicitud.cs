using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMI.Models
{
    public class cSolicitud
    {
        //aqui se supone que sera para dar de alta una solicitud.
        OPEntities contexto;
        private int idformato;
        private TbFormato tipoformato;

        private TbUsuario usuario;
        public cSolicitud()
        {
            idformato = 1;
            contexto= new OPEntities();

            tipoformato = contexto.TbFormato.Find(idformato);

            usuario = contexto.TbUsuario.Find(2);

            


        }


        public string Datos()
        {
            return usuario.Nombre + " : " + usuario.TbArea.Nombre;
        }
    }
}