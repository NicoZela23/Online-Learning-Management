using Online_Learning_Management.Domain.Entities;

public interface ICourseRepository
{
    Task<Course> CreateCourseAsync(Course course);
    Task<Course> GetCourseByIdAsync(Guid Id);
    Task<IEnumerable<Course>> GetCoursesByIdInstructorAsync(int IdInstructor);
    Task<bool> InstructorExistsAsync(int idInstructor);
    Task<Course> UpdateCourseAsync(Course course);
}
