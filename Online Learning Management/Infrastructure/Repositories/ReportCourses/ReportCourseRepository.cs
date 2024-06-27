using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.ReportCourses;
using Online_Learning_Management.Domain.Interfaces.ReportCourses;
using Online_Learning_Management.Infrastructure.Data;

namespace Online_Learning_Management.Infrastructure.Repositories.ReportCourses
{
    public class ReportCourseRepository : IReportCourseRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportCourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ReportCourse> GetReportCourseByStudentAndCourseAsync(Guid studentId, Guid courseId)
        {
            return await _context.ReportCourses
                                 .Where(rc => rc.StudentID == studentId && rc.CourseID == courseId)
                                 .FirstOrDefaultAsync();
        }
    }
}
