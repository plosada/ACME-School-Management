using Domain.Courses;
using Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> GetStudentAsync(int studentId)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
        }
    }
}
