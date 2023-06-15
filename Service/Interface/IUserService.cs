using Fochso.Models.Authorize;
using Fochso.Models.User;
using Fochso.Models;

namespace Fochso.Service.Interface
{
    public interface IUserService
    {
        UserResponseModel GetUser(int userId);
        BaseResponseModel AddUser(SignUpViewModel request, string roleName);
        UserResponseModel Login(string username, string password);
    }
}
