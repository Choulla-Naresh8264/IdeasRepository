using IdeasRepository.DAL.Contexts;
using System.Data.Entity.Migrations;

namespace IdeasRepository.DAL.Migrations
{
    /// <summary>
    /// Configuration relating to the use of migrations for a given database model.
    /// </summary>
    public class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
        }
    }
}
