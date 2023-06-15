using Fochso.Models;
using Fochso.Models.Class;

namespace Fochso.Service.Interface
{
    public interface IClassService
    {
        BaseResponseModel CreateClass(CreateClassViewModel createClass);
        BaseResponseModel DeleteClass(int classId);
        BaseResponseModel UpdateClass(int classId, UpdateClassViewModel updateClass);
        ClassResponseModel GetClass(int classId);
        ClassesResponseModel GetAllClass();



    }
}
