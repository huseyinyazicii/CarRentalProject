using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<User> Get(int id);
        IResult Add(User user);
        IResult Delete(User user);
        IResult Update(User user);

        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<User> GetByEmail(string email);
        IDataResult<UserBasicDto> GetByEmailDto(string email);
    }
}
