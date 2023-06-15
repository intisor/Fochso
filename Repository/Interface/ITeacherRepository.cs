using Fochso.Entities;
using Fochso.Repository;
using System.Linq.Expressions;

namespace Fochso.Repository.Interfaces
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        List<Teacher> GetTeachers();
        List<Teacher> GetTeachers(Expression<Func<Teacher, bool>> expression);
        Teacher GetTeacher(Expression<Func<Teacher, bool>> expression);
        List<Teacher> GetTeacherByTeacherId(int id);
    }
}
