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

            AddRecords(context, 7);

            context.SaveChanges();
        }

        private void AddRecords(ApplicationDbContext context, int recordsCount)
        {
            foreach (var user in context.Users)
            {
                for (int i = 0; i < recordsCount; i++)
                {
                    var record = new Record
                    {
                        Id = Guid.NewGuid().ToString(),
                        Author = user.UserName,
                        CreationDate = DateTime.Now,
                        TextBody = $"Text message {i} by {user.UserName} for testing purposes",
                        RecordTypeId = GetRandomRecordTypeId(context)
                    };

                    context.Records.Add(record);  
                }
            }
            context.SaveChanges();
        }

        private string GetRandomRecordTypeId(ApplicationDbContext context)
        {
            var random = new Random();
            var recordTypeNumber = random.Next(0, 5);

            return context.RecordTypes.ToList()[recordTypeNumber].Id;
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
