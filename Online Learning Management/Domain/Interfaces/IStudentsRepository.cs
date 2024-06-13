using Online_Learning_Management.Domain.Entities;

public interface IStudentsRepository
{
    Task<Students> GetStudentByIdAsync(int id);
    Task DeleteStudentByIdAsync(int id);
}