using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.Courses;
using Online_Learning_Management.Domain.Entities.Instructors;
using Online_Learning_Management.Domain.Interfaces.Instructors;
using Online_Learning_Management.Infrastructure.Data;

namespace Online_Learning_Management.Infrastructure.Repositories.Instructors
{
    public class InstructorRepository : INstructorRepository
    {
        private readonly ApplicationDbContext _context;
        public InstructorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Instructor> GetInstructorByIdAsync(Guid id)
        {
            return await _context.Instructors.FindAsync(id);
        }
        public async Task<IEnumerable<Instructor>> GetAllInstructorsAsync()
        {
            return await _context.Instructors.ToListAsync();
        }
        public async Task<Instructor> AddInstructorAsync(Instructor instructor)
        {
            var result = await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task UpdateInstructorAsync(Instructor instructor)
        {
            _context.Instructors.Update(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInstructorAsync(Guid id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Course>> GetCoursesByInstructorIdAsync(Guid id)
        {
            return await _context.Courses
                              .Where(course => course.IdInstructor == id)
                              .ToListAsync();
        }
    }
}
