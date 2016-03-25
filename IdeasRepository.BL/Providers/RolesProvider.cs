using IdeasRepository.BL.Interfaces;
using IdeasRepository.DAL.Managers;
using Ninject;
using System.Web;

namespace IdeasRepository.BL.Providers
{
    public class RolesProvider : IRolesManager
    {
        /// <summary>
        /// An instance of the database context.
        /// </summary>
        private HttpContextBase _context;

        [Inject]
        public ApplicationRoleManager RoleManager { get; set; }

        public RolesProvider(HttpContextBase context)
        {
            _context = context;
        }
    }
}
