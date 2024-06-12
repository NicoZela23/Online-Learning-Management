using Online_Learning_Management.Domain.Entities;

public interface ICourseRepository
{
    Task<Students> CreateStdentsAsync(Students student);
    Task<Students> GetStudentsByIdAsync(int Id);
}