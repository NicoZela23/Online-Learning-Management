using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.Students;
using Online_Learning_Management.Domain.Interfaces.Students;
using Online_Learning_Management.Infrastructure.Data;


namespace Online_Learning_Management.Infrastructure.Students
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            var result = await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task DeleteStudentAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }
        public async Task<Student> GetStudentByIdAsync(Guid id)
        {
            return await _context.Students.FindAsync(id);
        }
        public async Task UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

    }
}
