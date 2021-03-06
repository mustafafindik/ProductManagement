using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Core.Entities.Concrete;
using ProductManagement.Core.Utilities.Security.Hashing;
using ProductManagement.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using ProductManagement.Entities.Dtos;

namespace ProductManagement.DataAccess.Concrete.EntityFrameworkCore.Seeds
{
   public static  class CreteUserAndRoleSeed
    {
        public static void Seed(IApplicationBuilder app)
        {
           
            var context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            int roleId = 1;
            context.Database.Migrate();

            if (!context.Roles.Any())
            {
                var adminRole = new Role() {Name = "Admin", Description = "Admin Role"};
                context.Add(adminRole);
                context.SaveChanges();
                roleId = adminRole.Id;

            }

            if (!context.Users.Any())
            {
                HashingAndVerifyPasswordHelper.CreatePasswordHash("123123", out var passwordHash, out var passwordKey);
                var user = new User
                {
                    Email = "Admin@Admin.com",
                    FirstName = "Admin",
                    LastName = "Role",
                    PasswordHash = passwordHash,
                    PasswordKey = passwordKey,
                    IsActive = true
                };

                context.Add(user);
                context.SaveChanges();

                var userToRole = new UserRole() { RoleId = roleId, UserId = user.Id};
                context.Add(userToRole);
                context.SaveChanges();
             
            }





        }
    }
}
