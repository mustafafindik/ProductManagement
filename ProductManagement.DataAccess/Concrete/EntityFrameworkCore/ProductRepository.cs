using System;
using System.Collections.Generic;
using System.Text;
using ProductManagement.Core.DataAccess.EntityFrameworkCore;
using ProductManagement.DataAccess.Abstract;
using ProductManagement.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using ProductManagement.Entities.Concrete;

namespace ProductManagement.DataAccess.Concrete.EntityFrameworkCore
{
    public class ProductRepository : EntityRepository<Product, ApplicationDbContext>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
