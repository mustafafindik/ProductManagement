using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductManagement.DataAccess.Concrete.EntityFrameworkCore.Builders;
using ProductManagement.Entities.Concrete;

namespace ProductManagement.DataAccess.Concrete.EntityFrameworkCore.Contexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductBuilder());
            modelBuilder.ApplyConfiguration(new ProductImageBuilder());
         
        }
    }
}
