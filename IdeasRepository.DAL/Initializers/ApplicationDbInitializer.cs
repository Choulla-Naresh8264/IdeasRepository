﻿using IdeasRepository.DAL.Contexts;
using IdeasRepository.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IdeasRepository.DAL.Initializers
{
    /// <summary>
    /// Database initializer based on defining in the Entity Framework library
    /// wich drop, create and seed database always on the application starts.
    /// </summary>
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        private Random _random = new Random();

        protected override void Seed(ApplicationDbContext context)
        {
            FillDatabaseWithTestValues(context);
            base.Seed(context);
        }

        /// <summary>
        /// Fills the database underlying the given context with a test values.
        /// </summary>
        /// <param name="context">Database context.</param>
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

        /// <summary>
        /// Adds new records to the database to all users.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="recordsCount">Count of records that must be added to each user.</param>
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

        /// <summary>
        /// Gets Id of the random record type.
        /// </summary>
        /// <param name="context">Database context.</param>
        private string GetRandomRecordTypeId(ApplicationDbContext context)
        {
            var recordTypeNumber = _random.Next(0, 5);
            return context.RecordTypes.ToList()[recordTypeNumber].Id;
        }

        /// <summary>
        /// Adds an specific record types to the database.
        /// </summary>
        /// <param name="context">Database context.</param>
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

        /// <summary>
        /// Adds new user role.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="roleName">Name of the user role.</param>
        /// <returns>Returns Id of the just added record type.</returns>
        private string AddUserRole(ApplicationDbContext context, string roleName)
        {
            var userRole = new ApplicationRole { Name = roleName };
            context.Roles.Add(userRole);

            return userRole.Id;
        }

        /// <summary>
        /// Adds new user to the system.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="userName">User name.</param>
        /// <param name="userEmail">E-mail address.</param>
        /// <param name="userPassword">Unhashed password.</param>
        /// <param name="roleId">User role id.</param>
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
