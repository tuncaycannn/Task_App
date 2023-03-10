using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            _userDal.SaveChanges();
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Delete(int id)
        {
            var user = _userDal.Get(c => c.Id == id);

            if (user.Id != 0)
            {
                _userDal.Delete(user);
                _userDal.SaveChanges();
                return new SuccessResult(Messages.UserDeleted);
            }
            else
            {
                return new ErrorResult(Messages.UserNotDeleted);
            }
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UsersListed);
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>
               (_userDal.Get(c => c.Id == id));
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public IResult Update(UpdateUser updateUserDto)
        {

            var result = _userDal.Get(p => p.Id == updateUserDto.Id);
            if (result == null)
            {
                return new ErrorResult();
            }

            result.FirstName = updateUserDto.FirstName;
            result.LastName = updateUserDto.LastName;
            result.Status = updateUserDto.Status;
            result.Email = updateUserDto.Email;

            _userDal.Update(result);
            _userDal.SaveChanges();
            return new SuccessResult(Messages.UserUpdated);
        }

        public IResult UserChangePassword(UserChangePassword userChangePassword)
        {
            IResult result = BusinessRules.Run
                (
                IsThereAnyUser(userChangePassword.Email),
                VerifyPassword(userChangePassword.OldPassword, userChangePassword.Email),
                UserChangePassword(userChangePassword.Password, userChangePassword.ConfirmPassword, userChangePassword.Email));

            if (result != null)
            {
                return result;
            }
            _userDal.SaveChanges();
            return new SuccessResult(Messages.UserUpdated);
        }

        private IResult IsThereAnyUser(string email)
        {
            var isThereAnyUser = _userDal.Get(p => p.Email == email);
            if (isThereAnyUser == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            return new SuccessResult();
        }

        private IResult VerifyPassword(string oldPassword, string email)
        {
            var isThereAnyUser = _userDal.Get(p => p.Email == email);
            if (!HashingHelper.VerifyPasswordHash(oldPassword, isThereAnyUser.PasswordHash, isThereAnyUser.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordWrong);
            }
            return new SuccessResult();
        }
        private IResult UserChangePassword(string password, string confirmPassword, string email)
        {
            var isThereAnyUser = _userDal.Get(p => p.Email == email);

            if (password == confirmPassword)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                isThereAnyUser.PasswordHash = passwordHash;
                isThereAnyUser.PasswordSalt = passwordSalt;

                _userDal.Update(isThereAnyUser);
            }
            else
            {
                return new ErrorResult(Messages.NotTheSamePassword);
            }
            return new SuccessResult();
        }
    }
}
