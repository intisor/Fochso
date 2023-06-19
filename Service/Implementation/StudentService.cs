using Fochso.Models;
using Fochso.Models.Student;
using Fochso.Service.Interface;
using Fochso.Repository.Implementations;
using Fochso.Repository.Interfaces;
using Fochso.Entities;
using System.Linq.Expressions;

namespace Fochso.Service.Implementation
{
    public class StudentService : IStudentService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor; 
        }


        public BaseResponseModel CreateStudent(CreateStudentViewModel createStudent)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var isStudentExist = _unitOfWork.Students.Exists(s => s.Name == createStudent.Name);
            if (isStudentExist)
            {
                response.Message = "Student already exist!";
                return response;
            }
            if (createStudent == null)
            {
                response.Message = "Student field is required";
                return response;
            }

            var student = new Student
            {
                Name = createStudent.Name,
                Class = createStudent.Class,
                //ClassClass = createStudent.Class
                CreatedBy = createdBy
            };

            try
            {
                _unitOfWork.Students.Create(student);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Student created successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create student at this time: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel DeleteStudent(int studentId)
        {
            var response = new BaseResponseModel();
            var isStudentExist = _unitOfWork.Students.Exists(s => s.Id == studentId);

            if (!isStudentExist)
            {
                response.Message = "Student does not exist";
                return response;
            }
            var student = _unitOfWork.Students.Get(studentId);

            try
            {
                _unitOfWork.Students.Remove(student);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Student successfully deleted.";

                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public StudentsResponseModel GetAllStudent()
        {
            var response = new StudentsResponseModel();           
            try
            {
                var student = _unitOfWork.Students.GetAll();

                if (student is null) 
                {
                    response.Status = false;
                    response.Message = "No student found";
                    return response;
                }
                response.Data = student.Select(
                    student => new StudentViewModel
                    {
                        Id = student.Id,
                        Name = student.Name,
                        Class= student.Class
                    }).ToList();



            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            return response;
        }

        public StudentResponseModel GetStudent(int studentId)
        {
            var response = new StudentResponseModel();

            Expression<Func<Student, bool>> expression = s =>
                                              (s.Id == studentId);
            var isStudentExist = _unitOfWork.Students.Exists(expression);

            if (!isStudentExist)
            {
                response.Status = false;
                response.Message = $"Student with id {studentId} does not exist.";
                return response;
            }

            var student = _unitOfWork.Students.Get(studentId);
            response.Status = true;
            response.Message = "Success";
            response.Data = new StudentViewModel
            { Id = studentId,
             Name = student.Name,  
             Class = student.Class
            };

            return response;
        }

        public BaseResponseModel UpdateStudent(int studentId, UpdateStudentViewModel updateStudent)
        {
            var response = new BaseResponseModel();
            string modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var isStudentExist = _unitOfWork.Students.Exists(expression=> expression.Id == studentId);

            if (!isStudentExist)
            {
                response.Message = "Student does not exist";
                return response;
            }
            var student = _unitOfWork.Students.Get(studentId);
            student.Name = updateStudent.Name;
            student.Class = updateStudent.Class;

            try
            {
                _unitOfWork.Students.Update(student);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Student successfully updated";
                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
