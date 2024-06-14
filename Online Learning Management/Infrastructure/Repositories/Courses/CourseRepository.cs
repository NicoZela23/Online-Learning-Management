using Online_Learning_Management.Domain.Entities;
using Online_Learning_Management.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Online_Learning_Management.Infrastructure.Data;

namespace Online_Learning_Management.Infrastructure.Repositories.Courses
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            course.Id = Guid.NewGuid();
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }


        public async Task<Course> GetCourseByIdAsync(Guid Id)
        {
            return await _context.Courses.FindAsync(Id);
        }

        public async Task<IEnumerable<Course>> GetCoursesByIdInstructorAsync(int IdInstructor)
        {
            return await _context.Courses
                                 .Where(course => course.IdInstructor == IdInstructor)
                                 .ToListAsync();
        }

        public async Task<bool> InstructorExistsAsync(int idInstructor)
        {
            return await _context.Courses.AnyAsync(Course => Course.IdInstructor == idInstructor);
        }

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task DeleteCourseAsync(Guid courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }
    }
}