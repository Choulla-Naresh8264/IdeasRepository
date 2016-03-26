using IdeasRepository.DAL.Managers;
using System.Web;
using Microsoft.Owin.Security;
using IdeasRepository.BL.Interfaces;
using Ninject;

namespace IdeasRepository.BL.Providers
{
    public class AccountsProvider : IAccountsProvider
    {
        /// <summary>
        /// An instance of the database context.
        /// </summary>
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
    }
}
