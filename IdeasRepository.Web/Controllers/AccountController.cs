using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using IdeasRepository.Web.Models.Account;
using IdeasRepository.DAL.Entities;
using Microsoft.Owin.Security;
using IdeasRepository.BL.Interfaces;
using Ninject;

namespace IdeasRepository.Web.Controllers
{
    /// <summary>
    /// Describes the necessary actions to get user enter the system.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Provides access to the account management logic.
        /// </summary>
        [Inject]
        public IAccountsProvider _accountsProvider { get; set; }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
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
                        return RedirectToAction("List", "Record");

                    return Redirect(returnUrl);
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult Logout()
        {
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
    }
}
