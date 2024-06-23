using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Infrastructure.DTOs.Auth;

namespace Online_Learning_Management.Domain.Interfaces.Auth
{
    public interface IUserAuthRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User>GetUserByIdAsync(Guid id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
    }
}
