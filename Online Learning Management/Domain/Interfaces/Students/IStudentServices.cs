using Online_Learning_Management.Domain.Entities.Students;
using Online_Learning_Management.Infrastructure.DTOs.Student;

namespace Online_Learning_Management.Domain.Interfaces.Students
{
    public interface IStudentServices
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(Guid id);
        Task AddStudentAsync(CreateStudentDTO student);
        Task UpdateStudentAsync(Guid id, UpdateStudentDTO student);
        Task DeleteStudentAsync(Guid id);

    }
}
