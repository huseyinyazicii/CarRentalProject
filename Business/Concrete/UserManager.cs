using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
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
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            FluentValidationTool.Validate(new UserValidator(), user);

            try
            {
                _userDal.Add(user);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

        public IResult Delete(User user)
        {
            try
            {
                _userDal.Delete(user);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

        public IDataResult<User> Get(int id)
        {
            User user;
            try
            {
                user = _userDal.Get(u => u.Id == id);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<User>(exception.Message);
            }
            return new SuccessDataResult<User>(user);
        }

        public IDataResult<List<User>> GetAll()
        {
            List<User> users;
            try
            {
                users = _userDal.GetAll();
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<User>>(exception.Message);
            }
            return new SuccessDataResult<List<User>>(users);
        }

        public IDataResult<User> GetByEmail(string email)
        {
            User user = _userDal.Get(p => p.Email.ToLower() == email.ToLower());
            if (user == null)
            {
                return new ErrorDataResult<User>(Messages.GetErrorUserMessage);
            }
            else
            {
                return new SuccessDataResult<User>(user, Messages.GetSuccessUserMessage);
            }
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IResult Update(User user)
        {
            FluentValidationTool.Validate(new UserValidator(), user);

            User oldUser;
            try
            {
                oldUser = _userDal.Get(u => u.Id == user.Id);
                if(oldUser == null)
                {
                    return new ErrorResult("User not found");
                }

                oldUser.Email = user.Email != default ? user.Email : oldUser.Email;
                oldUser.FirstName = user.FirstName != default ? user.FirstName : oldUser.FirstName;
                oldUser.LastName = user.LastName != default ? user.LastName : oldUser.LastName;
                oldUser.PasswordHash = user.PasswordHash != default ? user.PasswordHash : oldUser.PasswordHash;
                oldUser.PasswordSalt = user.PasswordSalt != default ? user.PasswordSalt : oldUser.PasswordSalt;
                oldUser.Status = user.Status != default ? user.Status : oldUser.Status;

                _userDal.Update(oldUser);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }
    }
}
