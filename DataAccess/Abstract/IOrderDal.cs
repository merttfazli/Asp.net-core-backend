using Core.DataAccess;
using Entitites.Concrete;

namespace DataAccess.Abstract
{
    public interface IOrderDal:IEntityRepository<Order>
    {
    }
}
