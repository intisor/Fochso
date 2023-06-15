using Fochso.Entities;
using Fochso.Models;
using Fochso.Models.Class;
using Fochso.Repository.Interfaces;
using Fochso.Service.Interface;
using System.Linq.Expressions;

namespace Fochso.Service.Implementation
{
    public class ClassService : IClassService
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ClassService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;
		}

		public BaseResponseModel CreateClass(CreateClassViewModel createClass)
		{
			var response = new BaseResponseModel();
			var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
			var isClassExist = _unitOfWork.Classes.Exists(s => s.Name == createClass.Name);
			if (isClassExist)
			{
				response.Message = "class already exist!";
				return response;
			}
			if (createClass == null)
			{
				response.Message = "class field is required";
				return response;
			}

			var classes = new Class
			{
				Name = createClass.Name,
				//Students = ,
				Description= createClass.Description,
				CreatedBy = createdBy
			};

			try
			{
				_unitOfWork.Classes.Create(classes);
				_unitOfWork.SaveChanges();
				response.Status = true;
				response.Message = "Class created successfully.";

				return response;
			}
			catch (Exception ex)
			{
				response.Message = $"Failed to create class at this time: {ex.Message}";
				return response;
			}
		}

		public BaseResponseModel DeleteClass(int classId)
		{

			var response = new BaseResponseModel();
			var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
			var isClassExist = _unitOfWork.Classes.Exists(s => s.Id == classId);
			if (isClassExist)
			{
				response.Message = "class already exist!";
				return response;
			}
			var classes = _unitOfWork.Classes.Get(classId);

			try
			{
				_unitOfWork.Classes.Remove(classes);
				_unitOfWork.SaveChanges();
				response.Status = true;
				response.Message = "Class successfully deleted.";

				return response;
			}
			catch (Exception ex)
			{
				return response;
			}
		}

		public ClassesResponseModel GetAllClass()
		{
			var response = new ClassesResponseModel();
			try
			{
				var classes = _unitOfWork.Classes.GetAll();

				if (classes is null)
				{
					response.Status = false;
					response.Message = "No student found";
					return response;
				}
				response.Data = classes.Select(
					classes => new ClassViewModel
					{
						Id = classes.Id,
						Name = classes.Name,
						Description = classes.Description
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

		public ClassResponseModel GetClass(int classId)
		{
			var response = new ClassResponseModel();

			Expression<Func<Class, bool>> expression = s =>
											  (s.Id == classId);
			var isClassExist = _unitOfWork.Classes.Exists(expression);

			if (!isClassExist)
			{
				response.Status = false;
				response.Message = $"Class with id {classId} does not exist.";
				return response;
			}

			var classes = _unitOfWork.Classes.Get(classId);
			response.Status = true;
			response.Message = "Success";
			response.Data = new ClassViewModel
			{
				Id = classId,
				Name = classes.Name,
				Description = classes.Description
			};

			return response;
		}

		public BaseResponseModel UpdateClass(int classId, UpdateClassViewModel updateClass)
		{
			var response = new BaseResponseModel();
			string modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
			var isClassExist = _unitOfWork.Classes.Exists(expression => expression.Id == classId);

			if (!isClassExist)
			{
				response.Message = "Class does not exist";
				return response;
			}
			var classes = _unitOfWork.Classes.Get(classId);
			classes.Name = updateClass.Name;
			classes.Description = updateClass.Description;

			try
			{
				_unitOfWork.Classes.Update(classes);
				_unitOfWork.SaveChanges();
				response.Status = true;
				response.Message = "Class successfully updated";
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
