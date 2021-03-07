using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProductManagement.Core.DataAccess;
using ProductManagement.Core.Utilities.Results;
using ProductManagement.Entities.Concrete;

namespace ProductManagement.DataAccess.Abstract
{
    public interface IProductRepository: IEntityRepository<Product>
    {
        void AddProductImage(string dbPath, int productId);
        List<ProductImage> GetImagesById(int productId);
        void DeleteImage(int id);
    }
}
