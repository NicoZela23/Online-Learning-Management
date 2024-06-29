using AutoMapper;
using Online_Learning_Management.Domain.Entities.Post;
using Online_Learning_Management.Domain.Interfaces.Post;

using Online_Learning_Management.Infrastructure.DTOs.Post;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Learning_Management.Application.Post.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task AddPostAsync(CreatePostDTO createPostDTO)
        {
            var post = _mapper.Map<Posts>(createPostDTO);
            await _postRepository.AddPostAsync(post);
        }

        public async Task DeletePostAsync(Guid id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                throw new ArgumentException("Post not found.");
            }
            await _postRepository.DeletePostAsync(id);
        }

        public async Task<IEnumerable<Posts>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return _mapper.Map<IEnumerable<Posts>>(posts);
        }

        public async Task<Posts> GetPostByIdAsync(Guid id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                throw new ArgumentException("Post not found.");
            }
            return _mapper.Map<Posts>(post);
        }

        public async Task UpdatePostAsync(Guid id, UpdatePostDTO updatePostDTO)
        {
            var existingPost = await _postRepository.GetPostByIdAsync(id);
            if (existingPost == null)
            {
                throw new ArgumentException("Post not found.");
            }

            _mapper.Map(updatePostDTO, existingPost);
            await _postRepository.UpdatePostAsync(existingPost);
        }
    }
}
