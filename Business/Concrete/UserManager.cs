using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.Added);
        }

        public IDataResult<User> GetByMail(string email)
        {
            var data = _userDal.Get(x => x.Email == email);
            if (data != null)
                return new SuccessDataResult<User>(data);
            return new ErrorDataResult<User>(Messages.DataNotFound);
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var data = _userDal.GetClaims(user);
            if (data.Any())
                return new SuccessDataResult<List<OperationClaim>>(data);
            return new ErrorDataResult<List<OperationClaim>>(Messages.DataNotFound);
        }
    }
}
