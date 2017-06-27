using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OIM_DAL;

namespace OMI.Models.Utils
{
    public class MaptoGridModel
    {
        public static object MapToGridModel(TbPedidoM o)
        {
            return
                new
                {
                    o.Id,
                    o.Descripcion,
                    o.Cantidad,
                    IdSupervisor = o.Supervisores.TbUsuario.Nombre,
                    Estatus = o.TbStatusAutorizacion.Nombre,
                    Unidad = o.TbUnidad.Nombre,
                    Categoria = o.TbCategoria.Nombre,
                    Seleccionado = o.TbOrdenCompra.Nombre,
                   Observacion =o.Observacion,
                   Solicitud= o.IdSolicitud,
                   Proveedor= o.Proveedor

                };
        }


        public static object MapToGridModel(TbPedidoPersonal o)
        {
            return
                new
                {
                    o.Id,
                    Especialidad = o.TbEspecialidad.Nombre,

                    Profesion = o.TbProfesion.Nombre,
                    Categoria = o.TbCategoriaRH.Nombre,
                    o.Cantidad,
                    IdSupervisor = o.TbUsuario.Nombre,
                    Estatus = o.TbStatusAutorizacion.Nombre,
                   o.Descripcion

                };
        }

    }
}