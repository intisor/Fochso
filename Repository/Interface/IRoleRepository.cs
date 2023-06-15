using Fochso.Entities;
using System.Linq.Expressions;

namespace Fochso.Repository.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
       
		public interface IRoleRepository : IRepository<Role>
		{
			List<Role> GetRoles();
			List<Role> GetRoles(Expression<Func<Role, bool>> expression);

			Role GetRole(Expression<Func<Role, bool>> expression);
			//IEnumerable<Role> GetAllRoles();
		}
	}

}
