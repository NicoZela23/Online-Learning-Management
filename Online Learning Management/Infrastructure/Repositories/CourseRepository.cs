using Online_Learning_Management.Domain.Entities;
using Online_Learning_Management.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Online_Learning_Management.Infrastructure.Data;

namespace Online_Learning_Management.Infrastructure.Repositories
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
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            return await _context.Courses.FindAsync(courseId);
        }

      
    }
}
