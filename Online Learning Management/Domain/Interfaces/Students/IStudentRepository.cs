using Online_Learning_Management.Domain.Entities.Students;

namespace Online_Learning_Management.Domain.Interfaces.Students
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentByIdAsync(Guid id);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Guid id);
    }
}
