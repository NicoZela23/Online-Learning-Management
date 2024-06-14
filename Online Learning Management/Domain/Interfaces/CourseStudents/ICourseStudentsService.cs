
using Online_Learning_Management.Infrastructure.DTOs.CourseStudents;

namespace Online_Learning_Management.Domain.Interfaces.CourseStudents
{
    public interface ICourseStudentsService
    {
        Task<IEnumerable<CourseStudentDTO>> GetAllCourseStudentsAsync();
        Task<CourseStudentDTO> GetCourseStudentByIdAsync(Guid id);
        Task DeleteCourseStudentAsync(Guid id);
    }
}
