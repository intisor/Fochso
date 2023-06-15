using Fochso.Context;
using Fochso.Entities;
using Fochso.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fochso.Repository.Implementations
{
	public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
	{
		public TeacherRepository(FochsoContext context) : base(context)
		{
		}
		List<Teacher> ITeacherRepository.GetTeachers()
		{
			var teachers = _context.Teachers
						.Include(s => s.Name)
						.Include(s => s.Id)
						.Include(s => s.TeachingSubject)
						.Where(c => c.Id.Equals(c.Id))
						.ToList();

			return teachers;
		}

		List<Teacher> ITeacherRepository.GetTeachers(Expression<Func<Teacher, bool>> expression)
		{
			var teachers = _context.Teachers
						  .Where(expression)
						  .Include(s => s.Name)
						  .Include(s => s.TeachingSubject)
						  .Include(s => s.Id)
						  .ToList();

			return teachers;
		}

		public Teacher GetTeacher(Expression<Func<Teacher, bool>> expression)
		{
			var teacher = _context.Teachers
						.Include(c => c.Name)
						.Include(c => c.TeachingSubject)
						 .Include(u => u.Id)
						 .SingleOrDefault(expression);

			return teacher;
		}

		List<Teacher> ITeacherRepository.GetTeacherByTeacherId(int id)
		{
			var teachers = _context.Teachers
							.Include(sc => sc.Id)
							.Include(sc => sc.TeachingSubject)
							.Include(s => s.Name)
							.Where(s => s.Equals(id))
							.ToList();

			return teachers;
		}
	}
}
