using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagement.Entities.Concrete;

namespace ProductManagement.DataAccess.Concrete.EntityFrameworkCore.Builders
{
    public class ProductImageBuilder: IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Product).WithMany(c => c.ProductImages).HasForeignKey(p => p.ProductId);
        }
    }
}
