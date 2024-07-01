using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entitites.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category,Context>,ICategoryDal
    {
        
    }
}
