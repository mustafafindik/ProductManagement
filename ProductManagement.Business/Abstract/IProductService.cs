using System;
using System.Collections.Generic;
using System.Text;
using ProductManagement.Core.Utilities.Results;
using ProductManagement.Entities.Concrete;

namespace ProductManagement.Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product);
        IResult Delete(Product product);
        IResult Update(Product product);
    }
}
