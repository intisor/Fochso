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
           .Include(x => x.Role)
           //.Include(s => s.Role)
           //.ThenInclude(s => s.RoleName)
           .SingleOrDefault(expression)!;
        }

        public List<User> GetUsers()
        {
            var users = _context.Users
              .Include(s => s.Role)
              .ThenInclude(s => s.Id)
              .Include(s => s.Role)
                .ThenInclude(s => s.RoleName)
              .Include(s => s.UserName)
              .Include(s => s.Id)
              .ToList();

            return users;
        }

        public List<User> GetUsers(Expression<Func<User, bool>> expression)
        {
            var users = _context.Users
                .Include(s => s.Role)
                .ThenInclude(s => s.Id)
                .Include(s => s.Role)
                .ThenInclude(s => s.RoleName)
                .Include(s => s.Id)
                .Include(s => s.UserName)
                .ToList();

            return users;
        }
    }
}
