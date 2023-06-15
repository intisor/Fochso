using Fochso.Models;
using Fochso.Models.Teacher;

namespace Fochso.Service.Interface
{
    public interface ITeacherService
    {
        BaseResponseModel CreateTeacher(CreateTeacherViewModel createTeacher);
        BaseResponseModel DeleteTeacher(int teacherId);
        BaseResponseModel UpdateTeacher(int teacherId, UpdateTeacherViewModel updateteacher);
        TeacherResponseModel GetTeacher(int teacherId);
        TeachersResponseModel GetAllTeacher();
    }
}
