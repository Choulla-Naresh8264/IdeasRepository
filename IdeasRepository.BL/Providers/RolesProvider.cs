using IdeasRepository.BL.Interfaces;
using IdeasRepository.DAL.Managers;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IdeasRepository.BL.Providers
{
    public class RolesProvider : IRolesManager
    {
        private HttpContextBase _context;
        public ApplicationRoleManager RoleManager { get; set; }

        public RolesProvider(HttpContextBase context)
        {
            _context = context;
            RoleManager = _context.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        }
    }
}
