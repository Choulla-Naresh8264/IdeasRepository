using Microsoft.AspNet.Identity.EntityFramework;

namespace IdeasRepository.DAL.Entities
{
    /// <summary>
    /// Represents an Identity user role.
    /// </summary>
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        { }

        public ApplicationRole(string roleName) : base(roleName)
        { }
    }
}
