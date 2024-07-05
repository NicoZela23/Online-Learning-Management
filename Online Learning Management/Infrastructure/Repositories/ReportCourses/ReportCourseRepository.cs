using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.ReportCourses;
using Online_Learning_Management.Domain.Interfaces.ReportCourses;
using Online_Learning_Management.Infrastructure.Data;
using System.Threading.Tasks;

namespace Online_Learning_Management.Infrastructure.Repositories.ReportCourses
{
    public class ReportCourseRepository : IReportCourseRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportCourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Object> GetReportCourseByStudentAndCourseAsync(
            Guid studentId, Guid courseId
        )
        {
            var result = await (from g in _context.GradeStudents
                                join s in _context.Students on g.StudentId equals s.Id
                                join c in _context.Courses on g.CourseId equals c.Id
                                where g.StudentId == studentId && g.CourseId == courseId
                                select new
                                {
                                    Id = g.Id,
                                    NameStudent = s.Name,
                                    TitleCourse = c.Title,
                                    Score = g.Score
                                }).FirstOrDefaultAsync();
            return result;

        }
    }
}
