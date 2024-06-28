using Business.Abstract;
using DataAccess.Abstract;
using Entitites.Concrete;

namespace Business.Concrete
{
    public class OrderManager:IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
