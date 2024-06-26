using Online_Learning_Management.Domain.Entities.Instructors;
using Online_Learning_Management.Infrastructure.DTOs.Instructor;

namespace Online_Learning_Management.Domain.Interfaces.Instructors
{
    public interface INstructorService
    {
        Task<IEnumerable<Instructor>> GetAllInstructorsAsync();
        Task<Instructor> GetInstructorByIdAsync(Guid id);
        Task AddInstructorAsync(CreateInstructorDTO createInstructorDTO);
        Task UpdateInstructorAsync(Guid id, UpdateInstructorDTO updateInstructorDTO);
        Task DeleteInstructorAsync(Guid id);
    }
}
