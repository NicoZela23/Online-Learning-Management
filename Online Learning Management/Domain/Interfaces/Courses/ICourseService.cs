using Online_Learning_Management.Domain.Entities.Courses;

public interface ICourseService
{
    Task<Course> CreateCourseAsync(CreateCourseDTO courseDto);
    Task<Course> GetCourseByIdAsync(Guid Id);
    Task<IEnumerable<Course>> GetCoursesByIdInstructorAsync(int IdInstructor);
    Task<Course> UpdateCourseAsync(Guid courseId, UpdateCourseDTO courseDto);
    Task DeleteCourseAsync(Guid courseId);
}