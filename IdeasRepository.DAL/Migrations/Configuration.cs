using IdeasRepository.DAL.Contexts;
using System.Data.Entity.Migrations;

namespace IdeasRepository.DAL.Migrations
{
    public class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
        }
    }
}
