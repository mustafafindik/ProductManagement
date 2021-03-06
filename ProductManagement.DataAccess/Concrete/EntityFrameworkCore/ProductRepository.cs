using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.DataAccess.EntityFrameworkCore;
using ProductManagement.DataAccess.Abstract;
using ProductManagement.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using ProductManagement.Entities.Concrete;

namespace ProductManagement.DataAccess.Concrete.EntityFrameworkCore
{
    public class ProductRepository : EntityRepository<Product, ApplicationDbContext>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

       
    }
}
