using Online_Learning_Management.Infrastructure.DTOs.Post;
using Online_Learning_Management.Domain.Entities.Post;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Learning_Management.Domain.Interfaces.Post
{
    public interface IPostService
    {
        Task AddPostAsync(CreatePostDTO createPostDTO);
        Task DeletePostAsync(Guid id);
        Task<IEnumerable<Posts>> GetAllPostsAsync();
        Task<Posts> GetPostByIdAsync(Guid id);
        Task UpdatePostAsync(Guid id, UpdatePostDTO updatePostDTO);
    }
}
