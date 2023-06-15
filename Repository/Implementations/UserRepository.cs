using Fochso.Context;
using Fochso.Entities;
using Fochso.Repository.Implementations;
using Fochso.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fochso.Repository.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(FochsoContext context) : base(context)
        {
        }

        public User GetUser(Expression<Func<User, bool>> expression)
        {
            return _context.Users
                .Include(x => x.RoleId)
                .SingleOrDefault(expression)!;
        }
    }
}
