using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Interfaces.Forums;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Domain.Entities.Forums;

namespace Online_Learning_Management.Infrastructure.Repositories.Forum
{
    public class ForumRepository : IForumRepository
    {
        private readonly ApplicationDbContext _context;

        public ForumRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task AddForumAsync(Domain.Entities.Forums.Forum forum)
        {
            await _context.Forums.AddAsync(forum);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteForumAsync(Guid id)
        {
            var forum = await _context.Forums.FindAsync(id);
            if (forum != null)
            {
                _context.Forums.Remove(forum);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Domain.Entities.Forums.Forum>> GetAllForumsAsync()
        {
            return await _context.Forums.ToListAsync();
        }

        public async Task<Domain.Entities.Forums.Forum> GetForumByIdAsync(Guid id)
        {
            return await _context.Forums.FindAsync(id);
        }

        public async Task UpdateForumAsync(Domain.Entities.Forums.Forum forum)
        {
            _context.Forums.Update(forum);
            await _context.SaveChangesAsync();
        }
    }
}
