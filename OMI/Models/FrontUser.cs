using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OMI.Models
{
    public class FrontUser
    {
        /*   public static bool TienePermiso(RolesPermisos valor)
           {
               var usuario = FrontUser.Get();
               return !usuario.Rol.Permiso.Where(x => x.PermisoID == valor)
                   .Any();
           }
   
           public static Usuario Get()
           {
               return new Usuario().Obtener(SessionHelper.GetUser());
           }
           * /
   
           
       }
   
       public class SessionHelper
       {
       }
   
       public class Usuario
       {
          
           
       }
   
       public enum RolesPermisos
       {
           #region Alumnos
           Alumno_Puede_Crear_Nuevo_Registro = 2,
           Alumno_Puede_Eliminar_Registro = 3,
           Alumno_Puede_Visualizar_Un_Alumno = 4,
           #endregion
       }
       public class PermisoAttribute : ActionFilterAttribute
       {
           public RolesPermisos Permiso { get; set; }
   
           public override void OnActionExecuting(ActionExecutingContext filterContext)
           {
               base.OnActionExecuting(filterContext);
   
               if (!FrontUser.TienePermiso(this.Permiso))
               {
                   filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                   {
                       controller = "Home",
                       action = "Index"
                   }));
               }
           }
      */
    }
}