using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class FindeksManager : IFindeksService
    {
        private readonly IUserService _userService;

        public FindeksManager(IUserService userService)
        {
            _userService = userService;
        }

        public IDataResult<int> GetFindeksScore(int userId)
        {
            int findeksScore = 1400;
            return new SuccessDataResult<int>(findeksScore, Messages.FindeksCalculateCompleted);
        }
    }
}
