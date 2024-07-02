using Online_Learning_Management.Domain.Entities.Courses;

public interface ICourseRepository
{
    Task<Course> CreateCourseAsync(Course course);
    Task<Course> GetCourseByIdAsync(Guid Id);
    Task<IEnumerable<Course>> GetCoursesByIdInstructorAsync(Guid IdInstructor);
    Task<bool> InstructorExistsAsync(Guid idInstructor);
    Task<Course> UpdateCourseAsync(Course course);
    Task DeleteCourseAsync(Guid courseId);
}
