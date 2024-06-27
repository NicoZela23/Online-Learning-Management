using Online_Learning_Management.Domain.Entities.ReportCourses;

namespace Online_Learning_Management.Domain.Interfaces.ReportCourses
{
    public interface IReportCourseRepository
    {
        Task<ReportCourse> GetReportCourseByStudentAndCourseAsync(Guid studentId, Guid courseId);
    }
}
