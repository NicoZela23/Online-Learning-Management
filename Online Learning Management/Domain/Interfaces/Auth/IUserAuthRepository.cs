using Online_Learning_Management.Domain.Entities.Auth;

namespace Online_Learning_Management.Domain.Interfaces.Auth
{
    public interface IUserAuthRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User>GetUserByIdAsync(Guid id);
        Task<User> GetUserByLoginCredentials(UserLogin userLogin);
        Task<User>AddUserAsync(User user);
        Task<User>UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
    }
}
