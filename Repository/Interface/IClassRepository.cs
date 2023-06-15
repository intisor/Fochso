using Fochso.Entities;
using System.Linq.Expressions;

namespace Fochso.Repository.Interfaces
{
    public interface IClassRepository : IRepository<Class>
    {
        List<Class> GetClasses();
        List<Class> GetClasses(Expression<Func<Class, bool>> expression);
        Class GetClass(Expression<Func<Class, bool>> expression);
    }
}
