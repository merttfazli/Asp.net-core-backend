using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entitites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>> GetAll()
        {
            var data = _categoryDal.GetAll();
            if (data.Any())
                return new SuccessDataResult<List<Category>>(data);
            else
                return new ErrorDataResult<List<Category>>(Messages.DataNotFound);
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            var data = _categoryDal.Get(x => x.CategoryId == categoryId);
            if (data != null)
                return new SuccessDataResult<Category>(data);
            else
                return new ErrorDataResult<Category>(Messages.DataNotFound);
        }
    }
}
