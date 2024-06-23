using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Infrastructure.DTOs.Auth;

namespace Online_Learning_Management.Domain.Interfaces.Auth
{
    public interface IAuthUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task AddUserAsync(CreateUserDTO user);
        Task UpdateUserAsync(Guid id, UpdateUserDTO user);
        Task DeleteUserAsync(Guid id);
    }

}
