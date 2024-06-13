using Online_Learning_Management.Domain.Entities;
using System.Threading.Tasks;

namespace Online_Learning_Management.Domain.Interfaces
{
    public interface IStudentsRepository
    
    {
        Task<Students> GetStudentByIdAsync(int id);
        Task DeleteStudentByIdAsync(int id);
    }
}
