using Fochso.Entities;
using System.Linq.Expressions;

namespace Fochso.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(Expression<Func<User, bool>> expression);
    }
}
