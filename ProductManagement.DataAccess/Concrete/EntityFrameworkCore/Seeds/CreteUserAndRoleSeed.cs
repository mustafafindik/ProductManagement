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
using ProductManagement.Entities.Concrete;
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
                var admin = new User
                {
                    Email = "admin@admin.com",
                    FirstName = "Admin",
                    LastName = "User",
                    PasswordHash = passwordHash,
                    PasswordKey = passwordKey,
                    IsActive = true
                };

                context.Add(admin);
                context.SaveChanges();

                var userToRole = new UserRole() { RoleId = roleId, UserId = admin.Id};
                context.Add(userToRole);
                context.SaveChanges();

                var user = new User
                {
                    Email = "user@user.com",
                    FirstName = "Default",
                    LastName = "User",
                    PasswordHash = passwordHash,
                    PasswordKey = passwordKey,
                    IsActive = true
                };

                context.Add(user);
                context.SaveChanges();

            }

            if (!context.Products.Any())
            {
                var products = new[] {
                    new Product() { ProductName = "Ürün 1" , BarcodeNumber = 1234567890, Price = 30, Description = "Ürün 1 Açıklaması", Quantity = 12},
                    new Product() { ProductName = "Ürün 2" , BarcodeNumber = 23423424244, Price = 22, Description = "Ürün 2 Açıklaması", Quantity = 44},
                    new Product() { ProductName = "Ürün 3" , BarcodeNumber = 2424242424, Price = 66, Description = "Ürün 3 Açıklaması", Quantity = 1},
                    new Product() { ProductName = "Ürün 4" , BarcodeNumber = 1234244567890, Price = 11, Description = "Ürün 4 Açıklaması", Quantity = 34},
                    new Product() { ProductName = "Ürün 5" , BarcodeNumber = 124334567890, Price = 33, Description = "Ürün 5 Açıklaması", Quantity = 2},
                    new Product() { ProductName = "Ürün 6" , BarcodeNumber = 12344567890, Price = 66, Description = "Ürün 6 Açıklaması", Quantity = 27},

                };

                context.AddRange(products);
                context.SaveChanges();

                var productsimagges = new[] {
                    new ProductImage() { ProductId = products[0].Id, ImagePath = "Resources/Images/productdefault.png"},
                    new ProductImage() { ProductId = products[1].Id, ImagePath = "Resources/Images/productdefault.png"},
                    new ProductImage() { ProductId = products[2].Id, ImagePath = "Resources/Images/productdefault.png"},
                    new ProductImage() { ProductId = products[3].Id, ImagePath = "Resources/Images/productdefault.png"},
                    new ProductImage() { ProductId = products[4].Id, ImagePath = "Resources/Images/productdefault.png"},
                    new ProductImage() { ProductId = products[5].Id, ImagePath = "Resources/Images/productdefault.png"},


                };

                context.AddRange(productsimagges);
                context.SaveChanges();

            }





        }
    }
}
