using Online_Learning_Management.Domain.Entities.CourseStudent;

namespace Online_Learning_Management.Domain.Interfaces.CourseStudents;
public interface ICourseStudentsRepository 
{
    Task<IEnumerable<CourseStudent>> GetAllCourseStudentsAsync();
    Task<CourseStudent> GetCourseStudentByIdAsync(Guid Id);
    Task DeleteCourseStudentAsync(Guid courseStudentId);
    Task<CourseStudent> GetCourseStudentByStudentAndCourseAsync(Guid studentId, Guid courseId);
    Task AddCourseStudentAsync(CourseStudent courseStudent);
    Task<bool> CourseExistsAsync(Guid courseId);
    Task<bool> StudentExistsAsync(Guid studentId); 
}