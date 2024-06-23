using Online_Learning_Management.Domain.Entities.Auth;

namespace Online_Learning_Management.Domain.Interfaces.Auth
{
    public interface IAuthValidService
    {
        Task<User>Authenticate(UserLogin userlogin);
        String Generate(User user);
    }
}
