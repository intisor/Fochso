using Fochso.Entities;
using Fochso.Models;
using Fochso.Repository.Interfaces;
using System.Linq.Expressions;
using Fochso.Service.Interface;
using Fochso.Models.Teacher;

namespace Fochso.Service.Implementation
{
	public class TeacherService : ITeacherService
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public TeacherService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;
		}

		public BaseResponseModel CreateTeacher(CreateTeacherViewModel createTeacher)
		{
			var response = new BaseResponseModel();
			var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
			var isTeacherExist = _unitOfWork.Teachers.Exists(s => s.Name == createTeacher.Name);
			if (isTeacherExist)
			{
				response.Message = "Teacher already exist!";
				return response;
			}
			if (createTeacher == null)
			{
				response.Message = "Teacher field is required";
				return response;
			}

			var Teacher = new Teacher
			{
				Name = createTeacher.Name,
				Id = createTeacher.Id,
				CreatedBy = createdBy,
				TeachingSubject = createTeacher.TeachingSubject,
			};

			try
			{
				_unitOfWork.Teachers.Create(Teacher);
				_unitOfWork.SaveChanges();
				response.Status = true;
				response.Message = "Teacher created successfully.";

				return response;
			}
			catch (Exception ex)
			{
				response.Message = $"Failed to create Teacher at this time: {ex.Message}";
				return response;
			}
		}

		public BaseResponseModel DeleteTeacher(int teacherId)
		{
			var response = new BaseResponseModel();
			var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
			var isTeacherExist = _unitOfWork.Teachers.Exists(s => s.Id == teacherId);
			if (isTeacherExist)
			{
				response.Message = "Teacher already exist!";
				return response;
			}
			if (teacherId == 0)
			{
				response.Message = "Teacher field is required";
				return response;
			}

			var teacher = _unitOfWork.Teachers.Get(teacherId);

			try
			{
				_unitOfWork.Teachers.Remove(teacher);
				_unitOfWork.SaveChanges();
				response.Status = true;
				response.Message = "teacher successfully deleted.";

				return response;
			}
			catch (Exception ex)
			{
				return response;
			}
		}

		public TeachersResponseModel GetAllTeacher()
		{
			var response = new TeachersResponseModel();
			try
			{
				var teacher = _unitOfWork.Teachers.GetAll();

				if (teacher is null)
				{
					response.Status = false;
					response.Message = "No teacher found";
					return response;
				}
				response.Data = teacher.Select(
					teacher => new TeacherViewModel
					{
						Id = teacher.Id,
						Name = teacher.Name,
						TeachingSubject = teacher.TeachingSubject
					}).ToList();



			}
			catch (Exception ex)
			{
				response.Message = ex.Message;
				return response;
			}
			response.Status = true;
			response.Message = "Success";
			return response;
		}

		public TeacherResponseModel GetTeacher(int teacherId)
		{
			var response = new TeacherResponseModel();

			Expression<Func<Teacher, bool>> expression = t =>
											  (t.Id == teacherId);
			var isTeacherExist = _unitOfWork.Teachers.Exists(expression);

			if (!isTeacherExist)
			{
				response.Status = false;
				response.Message = $"teacher with id {teacherId} does not exist.";
				return response;
			}

			var teacher = _unitOfWork.Teachers.Get(teacherId);
			response.Status = true;
			response.Message = "Success";
			response.Data = new TeacherViewModel
			{
				Id = teacherId,
				Name = teacher.Name,
				TeachingSubject = teacher.TeachingSubject
			};

			return response;
		}

		public BaseResponseModel UpdateTeacher(int teacherId, UpdateTeacherViewModel updateTeacher)
		{
			var response = new BaseResponseModel();
			var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
			var isTeacherExist = _unitOfWork.Teachers.Exists(s => s.Id == teacherId);
			if (isTeacherExist)
			{
				response.Message = "Teacher already exist!";
				return response;
			}
			if (teacherId == 0)
			{
				response.Message = "Teacher field is required";
				return response;
			}

			var teacher = _unitOfWork.Teachers.Get(teacherId);
			teacher.Name = updateTeacher.Name;
			teacher.TeachingSubject = updateTeacher.TeachingSubject;

			try
			{
				_unitOfWork.Teachers.Update(teacher);
				_unitOfWork.SaveChanges();
				response.Status = true;
				response.Message = "teacher successfully updated";
				return response;
			}
			catch (Exception ex)
			{
				response.Message = ex.Message;
				return response;
			}
		}
	}
}
