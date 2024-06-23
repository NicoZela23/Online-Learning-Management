using Online_Learning_Management.Domain.Entities.CourseStudent;

namespace Online_Learning_Management.Domain.Interfaces.CourseStudents;
public interface ICourseStudentsRepository 
{
    Task<IEnumerable<CourseStudent>> GetAllCourseStudentsAsync();
    Task<CourseStudent> GetCourseStudentByIdAsync(Guid Id);
    Task DeleteCourseStudentAsync(Guid courseStudentId);
    
    // new method to get a student by student and course
    Task<CourseStudent> GetCourseStudentByStudentAndCourseAsync(Guid studentId, Guid courseId);

    Task AddCourseStudentAsync(CourseStudent courseStudent);
    
}