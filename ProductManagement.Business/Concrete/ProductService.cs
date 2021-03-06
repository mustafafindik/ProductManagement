using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProductManagement.Business.Abstract;
using ProductManagement.Business.Constants;
using ProductManagement.Core.Utilities.Results;
using ProductManagement.DataAccess.Abstract;
using ProductManagement.Entities.Concrete;

namespace ProductManagement.Business.Concrete
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IDataResult<List<Product>> GetAll()
        {
            var query = _productRepository.GetAll("ProductImages").ToList();
            return new SuccessDataResult<List<Product>>(query, Messages.ProductsGetSuccessfully);
        }

        public IDataResult<Product> GetById(int productId)
        {
            var query = _productRepository.Get(p => p.Id == productId, "ProductImages");
            return new SuccessDataResult<Product>(query, Messages.ProductGetSuccessfully);
        }

        public IResult Add(Product product)
        {
            _productRepository.Add(product);
            return new SuccessResult(Messages.ProductAddedSuccessfully);
        }

        public IResult Delete(Product product)
        {
            _productRepository.Delete(product,product.Id);
            return new SuccessResult(Messages.ProductDeletedSuccessfully);
        }

        public IResult Update(Product product)
        {
            _productRepository.Update(product,product.Id);
            return new SuccessResult(Messages.ProductUpdatedSuccessfully);
        }
    }
}
