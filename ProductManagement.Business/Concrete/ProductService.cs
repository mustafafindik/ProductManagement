using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using ProductManagement.Business.Abstract;
using ProductManagement.Business.Constants;
using ProductManagement.Core.Aspects.Autofac.Caching;
using ProductManagement.Core.Aspects.Autofac.Logging;
using ProductManagement.Core.Aspects.Autofac.Security;
using ProductManagement.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using ProductManagement.Core.Utilities.Results;
using ProductManagement.DataAccess.Abstract;
using ProductManagement.Entities.Concrete;

namespace ProductManagement.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }



        [CacheAspect]
      
        public IDataResult<List<Product>> GetAll()
        {
            var query = _productRepository.GetAll("ProductImages").ToList();
            return new SuccessDataResult<List<Product>>(query, Messages.ProductsGetSuccessfully);
        }


        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            var query = _productRepository.Get(p => p.Id == productId, "ProductImages");
            return new SuccessDataResult<Product>(query, Messages.ProductGetSuccessfully);
        }

        [CacheRemoveAspect("IProductService.Get")]
        [SecuredOperation("Admin", Priority = 1)]
        [LogAspect(typeof(MsSqlLogger))]
        public IResult Add(Product product)
        {
           
            _productRepository.Add(product);
            return new SuccessResult(Messages.ProductAddedSuccessfully);
        }

        [CacheRemoveAspect("IProductService.Get")]
        [SecuredOperation("Admin", Priority = 1)]
        [LogAspect(typeof(MsSqlLogger))]
        public IResult Delete(Product product)
        {
            _productRepository.Delete(product, product.Id);
            return new SuccessResult(Messages.ProductDeletedSuccessfully);
        }

        [CacheRemoveAspect("IProductService.Get")]
        [SecuredOperation("Admin", Priority = 1)]
        [LogAspect(typeof(MsSqlLogger))]
        public IResult Update(Product product)
        {
            _productRepository.Update(product, product.Id);
            return new SuccessResult(Messages.ProductUpdatedSuccessfully);
        }

        [CacheAspect]
        public IDataResult<List<ProductImage>> GetImagesById(int productId)
        {
            var query = _productRepository.GetImagesById(productId);
            return new SuccessDataResult<List<ProductImage>>(query, "Resimler Başarıyla alındı");
        }

        [CacheRemoveAspect("IProductService.Get")]
        [SecuredOperation("Admin", Priority = 1)]
        [LogAspect(typeof(MsSqlLogger))]
        public IResult UploadImage(int productId, IFormFile file, string folderName, string pathToSave)
        {
            
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName.ToString());
                var dbPath = Path.Combine(folderName, fileName.ToString());
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                _productRepository.AddProductImage(dbPath, productId);

                return new SuccessResult("Resim Eklendi.");

        }

        [CacheRemoveAspect("IProductService.Get")]
        [SecuredOperation("Admin", Priority = 1)]
        [LogAspect(typeof(MsSqlLogger))]
        public IResult DeleteImage(int id)
        {
            _productRepository.DeleteImage(id);
            return new SuccessResult("Ürün Resmi Silindi.");
        }
    }
}
