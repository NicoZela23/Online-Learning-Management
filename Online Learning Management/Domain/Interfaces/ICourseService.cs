using Online_Learning_Management.Domain.Entities;

public interface ICourseService
{
    Task<Course> CreateCourseAsync(CreateCourseDTO courseDto);
    Task<Course> GetCourseByIdAsync(Guid Id);
}