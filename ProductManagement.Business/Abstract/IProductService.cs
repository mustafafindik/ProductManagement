using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
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
        IDataResult<List<ProductImage>> GetImagesById(int productId);
        IResult UploadImage(int productId,IFormFile file, string folderName,string pathToSave);
        IResult DeleteImage(int id);
    }
}
