using Online_Learning_Management.Domain.Entities.ReportCourses;

namespace Online_Learning_Management.Domain.Interfaces.ReportCourses
{
    public interface IReportCourseRepository
    {
        Task<ReportCourse> GetReportCourseByIdAsync(Guid Id);
    }
}
