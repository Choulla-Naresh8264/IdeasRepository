using IdeasRepository.BL.Interfaces;
using IdeasRepository.DAL.Entities;
using IdeasRepository.Web.Models.Roles;
using Microsoft.AspNet.Identity;
using Ninject;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IdeasRepository.Web.Controllers
{
    public class RolesController : Controller
    {
        [Inject]
        public IRolesManager _rolesProvider { get; set; }

        public ActionResult List()
        {
            var roles = _rolesProvider.RoleManager.Roles;
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
                var result = await _rolesProvider.RoleManager.CreateAsync(
                    new ApplicationRole
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
            ApplicationRole role = await _rolesProvider.RoleManager.FindByIdAsync(id);
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
                ApplicationRole role = await _rolesProvider.RoleManager.FindByIdAsync(model.Id);
                if (role != null)
                {
                    role.Name = model.Name;
                    IdentityResult result = await _rolesProvider.RoleManager.UpdateAsync(role);
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
            ApplicationRole role = await _rolesProvider.RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _rolesProvider.RoleManager.DeleteAsync(role);
            }
            return RedirectToAction("List");
        }
    }
}