using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.CourseStudent;
using Online_Learning_Management.Domain.Interfaces.CourseStudents;
using Online_Learning_Management.Infrastructure.Data;


namespace Online_Learning_Management.Infrastructure.Repositories.CourseStudents
{
    public class CourseStudentsRepository : ICourseStudentsRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseStudentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CourseStudent> GetCourseStudentByIdAsync(Guid id)
        {
            return await _context.CourseStudents.FindAsync(id);
        }

        public async Task<IEnumerable<CourseStudent>> GetAllCourseStudentsAsync()
        {
            return await _context.CourseStudents.ToListAsync();
        }

        public async Task DeleteCourseStudentAsync(Guid id)
        {
            var courseStudent = await _context.CourseStudents.FindAsync(id);
            if (courseStudent != null)
            {
                _context.CourseStudents.Remove(courseStudent);
                await _context.SaveChangesAsync();
            }
        }
        // new method to get a student by student and course
        public async Task<CourseStudent> GetCourseStudentByStudentAndCourseAsync(Guid studentId, Guid courseId)
        {
            return await _context.CourseStudents
                .FirstOrDefaultAsync(cs => cs.StudentID == studentId && cs.CourseID == courseId);
        }

        public Task AddCourseStudentAsync(CourseStudent courseStudent)
        {
            _context.CourseStudents.Add(courseStudent);
            return _context.SaveChangesAsync();
        }
    }
}