using Core.Entities.Concrete;
using Core.Utilities.Results;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IResult Add(User user);
        IResult Delete(int id);
        IResult Update(UpdateUser updateUserDto);
        IDataResult<User> GetById(int id);
        List<OperationClaim> GetClaims(User user);
        User GetByMail(string email);
        IResult UserChangePassword(UserChangePassword userChangePassword);
    }
}
