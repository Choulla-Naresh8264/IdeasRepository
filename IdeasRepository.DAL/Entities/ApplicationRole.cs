using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeasRepository.DAL.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        { }

        public ApplicationRole(string roleName) : base(roleName)
        { }
    }
}
