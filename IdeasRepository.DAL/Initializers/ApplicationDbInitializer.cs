using IdeasRepository.DAL.Contexts;
using IdeasRepository.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeasRepository.DAL.Initializers
{
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            FillDatabaseWithTestValues(context);
            base.Seed(context);
        }

        private void FillDatabaseWithTestValues(ApplicationDbContext context)
        {
            var roleIdAdmin = AddUserRole(context, "Administrator");
            var roleIdUser = AddUserRole(context, "User");

            AddUser(context, "admin", "admin@admin.com", "admin", roleIdAdmin);
            AddUser(context, "user", "user@user.com", "user", roleIdUser);

            AddRecordTypes(context);

            context.SaveChanges();
        }

        private void AddRecordTypes(ApplicationDbContext context)
        {
            var recordTypes = new List<RecordType>();
            recordTypes.Add(new RecordType { Id = Guid.NewGuid().ToString(), Name = "Note" });
            recordTypes.Add(new RecordType { Id = Guid.NewGuid().ToString(), Name = "Though" });
            recordTypes.Add(new RecordType { Id = Guid.NewGuid().ToString(), Name = "Idea" });
            recordTypes.Add(new RecordType { Id = Guid.NewGuid().ToString(), Name = "Remark" });
            recordTypes.Add(new RecordType { Id = Guid.NewGuid().ToString(), Name = "Comment" });

            context.RecordTypes.AddRange(recordTypes);
            context.SaveChanges();
        }

        private string AddUserRole(ApplicationDbContext context, string roleName)
        {
            var userRole = new ApplicationRole { Name = roleName };
            context.Roles.Add(userRole);

            return userRole.Id;
        }

        private void AddUser(ApplicationDbContext context, string userName, string userEmail, string userPassword, string roleId)
        {
            var hasher = new PasswordHasher();

            var user = new ApplicationUser
            {
                UserName = userName,
                PasswordHash = hasher.HashPassword(userPassword),
                Email = userEmail,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            user.Roles.Add(new IdentityUserRole { RoleId = roleId, UserId = user.Id });

            context.Users.Add(user);
        }
    }
}
