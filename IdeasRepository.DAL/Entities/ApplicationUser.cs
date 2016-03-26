using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace IdeasRepository.DAL.Entities
{
    /// <summary>
    /// Represents an extended Identity user.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
            Records = new HashSet<Record>();
        }

        public virtual ICollection<Record> Records { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}
