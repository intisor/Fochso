using Fochso.Entities;
using System.Linq.Expressions;

namespace Fochso.Repository
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        T Create(T entity);
        T Get(int id);
        T Update(T entity);
        void Remove(T entity);
        List<T> GetAllByIds(List<int> ids);
        T Get(Expression<Func<T, bool>> expression);
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression);
        bool Exists(Expression<Func<T, bool>> expression);
        IReadOnlyList<T> SelectAll();
        IReadOnlyList<T> SelectAll(Expression<Func<T, bool>> expression);
    }
}
