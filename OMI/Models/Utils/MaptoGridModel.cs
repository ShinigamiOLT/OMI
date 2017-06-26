﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                    Categoria = o.TbCategoria.Nombre

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