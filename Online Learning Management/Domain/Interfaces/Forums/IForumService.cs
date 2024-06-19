using Online_Learning_Management.Domain.Entities.Forums;
using Online_Learning_Management.Infrastructure.DTOs.Forum;


namespace Online_Learning_Management.Domain.Interfaces.Forums
{
    public interface IForumService
    {
        Task<IEnumerable<Forum>> GetAllForumsAsync();
        Task<Forum> GetForumByIdAsync(Guid id);
        Task AddForumAsync(CreateForumDTO forum);
        Task UpdateForumAsync(Guid id, UpdateForumDTO forum);
        Task DeleteForumAsync(Guid id);
    }
}
