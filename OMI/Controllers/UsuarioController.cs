using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OMI.Models;
using OMI.Models.Utils;

namespace OMI.Controllers
{
    public class UsuarioController : CatalogoController<ApplicationUser,cUsuario,ApplicationDbContext>
    {
        ApplicationDbContext context = new ApplicationDbContext();
        protected override cUsuario FillViewModel(ApplicationUser con)
        {
            return new cUsuario()
            {
                Id = con.Id,
                
                FullName = con.FirstName + " " + con.LastName,
                Usuario = con.UserName,
                Email = con.Email
            };
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            ViewBag.RoleId = new SelectList(roleManager.Roles.ToList(), "Name", "Name");
               OPEntities db = new OPEntities();
          ViewBag.Areas=new SelectList( db.TbArea.ToList(),"Id","Nombre");

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            try
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = model.UserName,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Nivel = (int)(model.Nivel != null ? (int?)model.Nivel.Value : 0)
                     
                    };
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        if (model.selectedRoles != null)
                        {
                            var result2 = await userManager.AddToRolesAsync(user.Id, model.selectedRoles);

                            if (!result2.Succeeded)
                            {
                                ModelState.AddModelError("", result.Errors.First());
                                ViewBag.RoleId = new SelectList(await roleManager.Roles.ToListAsync(), "Name", "Name");
                                return View();
                            }
                            userManager.SetLockoutEnabled(user.Id, false);
                            MessageToIndexManager
                                .SetMessage(
                                    TempData,
                                    string.Format("El usuario <strong>{0}</strong> fue creado con éxito", model.UserName),
                                    MessageToIndexType.Success
                                );
                            return RedirectToAction("Index", "Usuario");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", result.Errors.First());
                        ViewBag.RoleId = new SelectList(roleManager.Roles, "Name", "Name");
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            ViewBag.RoleId = new SelectList(roleManager.Roles.ToList(), "Name", "Name");

            return View(model);
        }
    }
}