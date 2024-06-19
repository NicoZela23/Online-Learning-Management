using Online_Learning_Management.Domain.Entities.Forums;

namespace Online_Learning_Management.Domain.Interfaces.Forums
{
    public interface IForumRepository
    {
        Task<Forum> GetForumByIdAsync(Guid id);
        Task<IEnumerable<Forum>> GetAllForumsAsync();
        Task AddForumAsync(Forum forum);
        Task UpdateForumAsync(Forum forum);
        Task DeleteForumAsync(Guid id);
    }
}
