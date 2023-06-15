using Fochso.Models;
using Fochso.Models.Student;

namespace Fochso.Service.Interface
{
    public interface IStudentService
    {
        BaseResponseModel CreateStudent(CreateStudentViewModel createStudent);
        BaseResponseModel DeleteStudent(int studentId);
        BaseResponseModel UpdateStudent(int studentId, UpdateStudentViewModel updateStudent);
        StudentResponseModel GetStudent(int studentId);
        StudentsResponseModel GetAllStudent();
    }
}
