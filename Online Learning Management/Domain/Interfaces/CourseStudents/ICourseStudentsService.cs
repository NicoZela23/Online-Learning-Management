using Online_Learning_Management.Infrastructure.DTOs.CourseStudents;
using Online_Learning_Management.Domain.Entities.CourseStudent;

namespace Online_Learning_Management.Domain.Interfaces.CourseStudents
{
    public interface ICourseStudentsService
    {
        Task<IEnumerable<CourseStudent>> GetAllCourseStudentsAsync();
        Task<CourseStudent> GetCourseStudentByIdAsync(Guid id);
        Task DeleteCourseStudentAsync(Guid id);
        Task WithdrawCourseStudentAsync(Guid studentId, Guid courseId);
        Task EnrollCourseStudentAsync(EnrollStudentDTO enrollStudentDTO);
    }
}
