using Fochso.Entities;
using Fochso.Models.User;
using Fochso.Models;
using Fochso.Repository.Interfaces;
using Fochso.Service.Interface;
using Fochso.Models.Authorize;
using Microsoft.AspNetCore.Mvc;
using AspNetCore;

namespace Fochso.Service.Implementations
{
	public class UserService : IUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IUnitOfWork _unitOfWork;

		public UserService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
		{
			_httpContextAccessor = httpContextAccessor;
			_unitOfWork = unitOfWork;
		}

		public BaseResponseModel AddUser(SignUpViewModel request)
		{
			var response = new BaseResponseModel();
			var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
			var userExist = _unitOfWork.Users.Exists(x => x.UserName == request.UserName || x.Email == request.Email);

			if (userExist)
			{
				response.Message = $"User with {request.UserName} or {request.Email} already exist";
				return response;
			}

			string roleName = "AppUser";

			var role = _unitOfWork.Roles.Get(x => x.RoleName == roleName);

			if (role is null)
			{
				response.Message = $"Role does not exist";
				return response;
			}

			var user = new User
			{
				UserName = request.UserName,
				Email = request.Email,
				Password = request.Password,
				RoleId = 2,
				RoleName= roleName,
				CreatedBy = createdBy,
			};

			try
			{
				_unitOfWork.Users.Create(user);
				_unitOfWork.SaveChanges();
				response.Message = $"You have succesfully signed up on Fochso School Application";
				response.Status = true;

				return response;
			}
			catch (Exception ex)
			{
				return new BaseResponseModel
				{
					Message = $"Unable to signup, an error occurred {ex.Message}"
				};
			}
		}

		public UserResponseModel GetUser(int userId)
		{
			var response = new UserResponseModel();
			var user = _unitOfWork.Users.GetUser(x => x.Id == userId);

			if (user is null)
			{
				response.Message = $"User with {userId} does not exist";
				return response;
			}

			response.Data = new UserViewModel
			{
				//UserId = user.Id,
				UserName = user.UserName,
				//Password = user.Password,
				Email = user.Email,
				RoleId = user.RoleId,
				RoleName = user.Role.RoleName,
			};
			response.Message = $"User successfully retrieved";
			response.Status = true;

			return response;
		}

		public UserResponseModel Login(LoginViewModel model)
		{
			var response = new UserResponseModel();

			try
			{
				var user = _unitOfWork.Users.GetUser(x =>
								(x.UserName.ToLower() == model.UserName.ToLower()
								|| x.Email.ToLower() == model.UserName.ToLower()));

				if (user is null)
				{
					response.Message = $"Account does not exist!";
					return response;
				}

				response.Data = new UserViewModel
				{
					Id = user.Id,
					//Password = user.Password,
					UserName = user.UserName,
					Email = user.Email,
					RoleId = user.RoleId,
					RoleName = user.Role.RoleName,
				};
				response.Message = $"Welcome {user.UserName}";
				response.Status = true;

				return response;
			}
			catch (Exception ex)
			{
				response.Message = $"An error occured: {ex.Message}";
				return response;
			}
		}
	}
}
