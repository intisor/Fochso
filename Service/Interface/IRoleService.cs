using Fochso.Models.Role;
using Fochso.Models;

namespace Fochso.Service.Interface
{
    public interface IRoleService
    {
        BaseResponseModel CreateRole(CreateRoleViewModel request);
        BaseResponseModel DeleteRole(int roleId);
        BaseResponseModel UpdateRole(int roleId, UpdateRoleViewModel request);
        RoleResponseModel GetRole(int roleId);
        RolesResponseModel GetAllRole();
    }
}
