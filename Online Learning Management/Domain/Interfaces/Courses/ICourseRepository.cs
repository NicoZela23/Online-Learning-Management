using Online_Learning_Management.Domain.Entities.Courses;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllCoursesAsync();
    Task<Course> CreateCourseAsync(Course course);
    Task<Course> GetCourseByIdAsync(Guid Id);
    Task<bool> InstructorExistsAsync(Guid idInstructor);
    Task<Course> UpdateCourseAsync(Course course);
    Task DeleteCourseAsync(Guid courseId);
    Task<IEnumerable<Course>> GetAllCoursesAsync(string search);
}
