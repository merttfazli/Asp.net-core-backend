using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcers.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entitites.Concrete;
using Entitites.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName), CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfCurrentCategoryCountUpperThen15());

            if (result != null)
            {
                return result;
            }
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

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {

            if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {
                if (CheckIfProductNameExists(product.ProductName).Success)
                {
                    _productDal.Update(product);
                    return new SuccessResult(Messages.Updated);
                }
                return new ErrorResult();
            }
            return new ErrorResult();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var data = _productDal.GetAll(x => x.CategoryId == categoryId).Count;
            if (data >= 10)
                return new ErrorResult(Messages.MaxProductCountReachedForThisCategory);
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var data = _productDal.GetAll(x => x.ProductName == productName);
            if (data.Any())
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCurrentCategoryCountUpperThen15()
        {
            var data = _categoryService.GetAll();
            if (data.Data.Count > 15)
                return new ErrorResult(Messages.MaxCategoryCountReached);
            return new SuccessResult();
        }
    }
}
