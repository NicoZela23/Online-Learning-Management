using Online_Learning_Management.Domain.Entities.Post;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Learning_Management.Domain.Interfaces.Post
{
    public interface IPostRepository
    {
        Task AddPostAsync(Posts post);
        Task DeletePostAsync(Guid id);
        Task<IEnumerable<Posts>> GetAllPostsAsync();
        Task<Posts> GetPostByIdAsync(Guid id);
        Task UpdatePostAsync(Posts post);
    }
}
