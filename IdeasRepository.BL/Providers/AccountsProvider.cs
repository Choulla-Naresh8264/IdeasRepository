using IdeasRepository.DAL.Managers;
using System.Web;
using Microsoft.Owin.Security;
using IdeasRepository.BL.Interfaces;
using Ninject;

namespace IdeasRepository.BL.Providers
{
    public class AccountsProvider : IAccountsProvider
    {
        private HttpContextBase _context;

        [Inject]
        public ApplicationUserManager UserManager { get; set; }
        
        [Inject]
        public ApplicationRoleManager RoleManager { get; set; }

        [Inject]
        public IAuthenticationManager AuthManager { get; set; }

        public AccountsProvider(HttpContextBase context)
        {
            _context = context;
        }

        //public async void SignIn(ApplicationUser user)
        //{
        //    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
        //                                    DefaultAuthenticationTypes.ApplicationCookie);
        //    AuthManager.SignOut();
        //    AuthManager.SignIn(new AuthenticationProperties
        //    {
        //        IsPersistent = true
        //    }, claim);
        //}

        //public async Task<ApplicationUser> FindUserAsync(string userName, string userPassword)
        //{
        //    var user = await UserManager.FindAsync(userName, userPassword);
        //    return user;
        //}

        //public async Task<ApplicationUser> Find(string userName, string userPassword)
        //{
        //    var user = await UserManager.FindAsync(userName, userPassword);
        //    return user;
        //}

        //public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string userPassword)
        //{
        //    var result = await UserManager.CreateAsync(user, userPassword);

        //    return result;
        //}


        //public void SignOut()
        //{
        //    AuthManager.SignOut();
        //}


    }
}
