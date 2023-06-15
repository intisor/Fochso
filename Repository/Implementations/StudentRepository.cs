using Fochso.Context;
using Fochso.Entities;
using Fochso.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fochso.Repository.Implementations
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(FochsoContext context) : base(context)
        {
        }

		public Student GetStudent(Expression<Func<Student, bool>> expression)
		{
			var students = _context.Students
			.Include(c => c.Name)
			.Include(c => c.Class)
			 .Include(u => u.Id)
			 .SingleOrDefault(expression);

			return students;
		}

		public List<Student> GetStudents()
		{
			var students = _context.Students
			.Include(s => s.Name)
			.Include(s => s.Id)
			.Include(s => s.Class)
			.Where(c => c.Id.Equals(c.Id))
			.ToList();

			return students;
		}

		public List<Student> GetStudents(Expression<Func<Student, bool>> expression)
		{
			var students = _context.Students
			  .Where(expression)
			  .Include(s => s.Name)
			  .Include(s => s.Class)
			  .Include(s => s.Id)
			  .ToList();

			return students;
		}

		public List<Student> GetStudentsByClassId(int id)
		{
			var studentclass = _context.Students
				.Include(sc => sc.ClassClass)
				.Include(sc => sc.Class)
				.Include(s => s.Name)
				.Include(s => s.Id)
				.Where(s => s.Id.Equals(id))
				.ToList();

			return studentclass;
		}

	}
}
