using Online_Learning_Management.Domain.Entities.Courses;

public interface ICourseService
{
    Task<IEnumerable<Course>> GetAllCoursesAsync(string search);
    Task<Course> CreateCourseAsync(CreateCourseDTO courseDto);
    Task<Course> GetCourseByIdAsync(Guid Id);
    Task<Course> UpdateCourseAsync(Guid courseId, UpdateCourseDTO courseDto);
    Task DeleteCourseAsync(Guid courseId);
}