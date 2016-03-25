using IdeasRepository.DAL.Entities;
using IdeasRepository.DAL.Initializers;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace IdeasRepository.DAL.Contexts
{
    /// <summary>
    /// Database context which extend base class with new sets of the entities
    /// and explicit determines database initializer.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("IdeasRepository", throwIfV1Schema: false)
        { }

        public DbSet<Record> Records { get; set; }
        public DbSet<RecordType> RecordTypes { get; set; }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
