using Fochso.Entities;
using System.Linq.Expressions;

namespace Fochso.Repository.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        List<Student> GetStudents();
        List<Student> GetStudents(Expression<Func<Student, bool>> expression);
        Student GetStudent(Expression<Func<Student, bool>> expression);
        List<Student> GetStudentsByClassId(int id);

    }
}