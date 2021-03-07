using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.DataAccess.EntityFrameworkCore;
using ProductManagement.Core.Utilities.Results;
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


        public void AddProductImage(string dbPath, int productId)
        {
            ProductImage productImage = new ProductImage();
            productImage.ImagePath = dbPath;
            productImage.ProductId = productId;
            _context.Add(productImage);
            _context.SaveChanges();
        }

        public List<ProductImage> GetImagesById(int productId)
        {
            return _context.ProductImages.Where(q => q.ProductId == productId).ToList();
        }
    }
}
