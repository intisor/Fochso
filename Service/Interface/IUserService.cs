using Fochso.Models.Authorize;
using Fochso.Models.User;
using Fochso.Models;
using Fochso.Entities;

namespace Fochso.Service.Interface
{
    public interface IUserService
    {
        UserResponseModel GetUser(int userId);
        BaseResponseModel AddUser(SignUpViewModel request);
        UserResponseModel Login(LoginViewModel request);
    }
}
