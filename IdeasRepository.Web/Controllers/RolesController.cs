using IdeasRepository.DAL.Entities;
using IdeasRepository.DAL.Managers;
using IdeasRepository.Web.Models.Roles;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdeasRepository.Web.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        public ActionResult List()
        {
            var roles = RoleManager.Roles;
            var rolesViewModel = new List<RoleViewModel>();
            
            foreach (var role in roles)
            {
                rolesViewModel.Add(new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                });
            }

            return View(rolesViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new ApplicationRole
                {
                    Name = model.Name,
                });
                if (result.Succeeded)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError("", "An error was ocurred, try again later");
                }
            }
            return View(model);
        }
        
        public async Task<ActionResult> Edit(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                return View(new RoleEditModel { Id = role.Id, Name = role.Name });
            }
            return RedirectToAction("List");
        }
        
        [HttpPost]
        public async Task<ActionResult> Edit(RoleEditModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = await RoleManager.FindByIdAsync(model.Id);
                if (role != null)
                {
                    role.Name = model.Name;
                    IdentityResult result = await RoleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("List");
                    }
                    else
                    {
                        ModelState.AddModelError("", "An error was ocurred, try again later");
                    }
                }
            }
            return View(model);
        }
        
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
            }
            return RedirectToAction("List");
        }
    }
}