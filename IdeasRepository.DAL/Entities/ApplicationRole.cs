using Microsoft.AspNet.Identity.EntityFramework;

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
