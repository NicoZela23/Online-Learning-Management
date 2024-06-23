using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Domain.Interfaces.Auth;
using Online_Learning_Management.Infrastructure.Data;

namespace Online_Learning_Management.Infrastructure.Repositories.Auth
{
    public class UserAuthRepository : IUserAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public UserAuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task <User> AddUserAsync(User user)
        {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task <User> UpdateUserAsync(User user)
        {
            var result = _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> GetUserByLoginCredentials(UserLogin userLogin)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == userLogin.Username && u.Password == userLogin.Password);
        }
    }
}
