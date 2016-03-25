﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using IdeasRepository.Web.Models.Account;
using IdeasRepository.DAL.Entities;
using Microsoft.Owin.Security;
using System.Security.Claims;
using IdeasRepository.DAL.Managers;
using IdeasRepository.BL.Providers;
using IdeasRepository.BL.Interfaces;

namespace IdeasRepository.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAccountsProvider _accountsProvider
        {
            get
            {
                return new AccountsProvider(HttpContext);
            }
        }

        //private ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //}

        //private ApplicationRoleManager RoleManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        //    }
        //}

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //if (ModelState.IsValid)
            //{
            //    ApplicationUser user = await UserManager.FindAsync(model.UserName, model.Password);
            //    if (user == null)
            //    {
            //        ModelState.AddModelError("", "Incorrect user name or password.");
            //    }
            //    else
            //    {
            //        ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
            //                                DefaultAuthenticationTypes.ApplicationCookie);
            //        AuthenticationManager.SignOut();
            //        AuthenticationManager.SignIn(new AuthenticationProperties
            //        {
            //            IsPersistent = true
            //        }, claim);
            //        if (string.IsNullOrEmpty(returnUrl))
            //            return RedirectToAction("Index", "Home");
            //        return Redirect(returnUrl);
            //    }
            //}
            //ViewBag.returnUrl = returnUrl;
            //return View(model);

            if (ModelState.IsValid)
            {
                var user = await _accountsProvider.UserManager.FindAsync(model.UserName, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Incorrect user name or password.");
                }
                else
                {
                    var claim = await _accountsProvider.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    _accountsProvider.AuthManager.SignOut();
                    _accountsProvider.AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");

                    return Redirect(returnUrl);
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult Logout()
        {
            //AuthenticationManager.SignOut();
            _accountsProvider.AuthManager.SignOut();

            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    ApplicationUser user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
            //    IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            //    if (result.Succeeded)
            //    {
            //        var currentUser = UserManager.FindByEmail(user.Email);
            //        var roleResult = UserManager.AddToRole(currentUser.Id, "User");

            //        return RedirectToAction("Login", "Account");
            //    }
            //    else
            //    {
            //        foreach (string error in result.Errors)
            //        {
            //            ModelState.AddModelError("", error);
            //        }
            //    }
            //}

            //return View(model);

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                var result = await _accountsProvider.UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var currentUser = _accountsProvider.UserManager.FindByEmail(user.Email);
                    var roleResult = _accountsProvider.UserManager.AddToRole(currentUser.Id, "User");

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return View(model);
        }
        /*
        public async Task<ActionResult> Edit()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                EditModel model = new EditModel { Year = user.Year };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditModel model)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                user.Year = model.Year;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }
        */
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            var user = await _accountsProvider.UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                var result = await _accountsProvider.UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Logout", "Account");
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}