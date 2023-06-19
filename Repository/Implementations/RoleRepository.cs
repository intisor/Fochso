using Fochso.Context;
using Fochso.Entities;
using Fochso.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fochso.Repository.Implementations
{
	public class RoleRepository : BaseRepository<Role>, IRoleRepository
	{
		public RoleRepository(FochsoContext context) : base(context)
		{
		}

		public List<Role> GetRoles()
		{
			var roles = _context.Roles
			  .Include(s => s.Users)
			  .ThenInclude(s => s.RoleId)
			  .Include(s => s.RoleName)
			  .Include(s => s.Id)
			  .ToList();

			return roles;
		}
		public	List<Role> GetRoles(Expression<Func<Role, bool>> expression)
		{
			var roles = _context.Roles
				 .Where(expression)
				 .Include(s => s.RoleName)
				 .Include(s => s.Id)
                .Include(s => s.Users)
				.Include(s => s.Id)
				.ToList();

				return roles;

		}
		public Role GetRole(Expression<Func<Role, bool>> expression)
		{
			var role = _context.Roles
				.Include(r => r.Id)
				.Include(r => r.RoleName)
				.Include(r => r.Description)
				.SingleOrDefault(expression);

			return role;

		}
		
	}
}

//			public void CreateRole(Role role)
//			{
//				// Code to create a new role in the database
//				_ .Roles.Add(role);
//				FochsoContext.SaveChanges();
//			}

//			public Role GetRoleById(int roleId)
//			{
//				// Code to retrieve a role by its ID from the database
//				return FochsoContext.Roles.FirstOrDefault(r => r.Id == roleId);
//			}

//			public IEnumerable<Role> GetAllRoles()
//			{
//				// Code to retrieve all roles from the database
//				return dbContext.Roles.ToList();
//			}

//			public void UpdateRole(Role role)
//			{
//				// Code to update an existing role in the database
//				dbContext.Roles.Update(role);
//				dbContext.SaveChanges();
//			}

//			public void DeleteRole(int roleId)
//			{
//				// Code to delete a role from the database
//				var role = GetRoleById(roleId);
//				if (role != null)
//				{
//					dbContext.Roles.Remove(role);
//					dbContext.SaveChanges();
//				}
//			}
//		}
//	}

//}
//    }
//}