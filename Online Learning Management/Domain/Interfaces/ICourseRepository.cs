using Online_Learning_Management.Domain.Entities;

public interface ICourseRepository
{
    Task<Course> CreateCourseAsync(Course course);
    Task<Course> GetCourseByIdAsync(Guid Id);
}
