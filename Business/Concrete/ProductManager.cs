using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entitites.Concrete;
using Entitites.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            if (product.ProductName.Length < 2)
                return new ErrorResult(Messages.ProductNameInvalid);

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Product>> GetAll()
        {
            var result = _productDal.GetAll();
            if (result.Any())
            {
                if (DateTime.Now.Hour == 22)
                    return new ErrorDataResult<List<Product>>(Messages.DataNotFound);
                return new SuccessDataResult<List<Product>>(result);
            }
            else
                return new ErrorDataResult<List<Product>>(Messages.DataNotFound);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            var result = _productDal.GetAll(x => x.CategoryId == categoryId);
            if (result.Any())
                return new SuccessDataResult<List<Product>>(result);
            else
                return new ErrorDataResult<List<Product>>(Messages.DataNotFound);
        }

        public IDataResult<Product> GetById(int productId)
        {
            var result = _productDal.Get(x => x.ProductId == productId);
            if (result != null)
                return new SuccessDataResult<Product>(result);
            return new ErrorDataResult<Product>(Messages.DataNotFound);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            var result = _productDal.GetAll(x => x.UnitPrice >= min && x.UnitPrice <= max);
            if (result.Any())
                return new SuccessDataResult<List<Product>>(result);
            return new ErrorDataResult<List<Product>>(Messages.DataNotFound);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetailDtos()
        {
            var result = _productDal.GetProductDetails();
            if (result.Any())
                return new SuccessDataResult<List<ProductDetailDto>>(result);
            return new ErrorDataResult<List<ProductDetailDto>>(Messages.DataNotFound);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.Updated);
        }
    }
}
