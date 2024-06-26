using Online_Learning_Management.Domain.Entities.Instructors;

namespace Online_Learning_Management.Domain.Interfaces.Instructors
{
    public interface INstructorRepository
    {
        Task<Instructor> GetInstructorByIdAsync(Guid id);
        Task<IEnumerable<Instructor>> GetAllInstructorsAsync();
        Task<Instructor> AddInstructorAsync(Instructor instructor);
        Task UpdateInstructorAsync(Instructor instructor);
        Task DeleteInstructorAsync(Guid id);
    }
}
