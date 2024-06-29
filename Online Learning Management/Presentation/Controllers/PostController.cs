using Microsoft.AspNetCore.Mvc;

using Online_Learning_Management.Infrastructure.DTOs.Post;
using Online_Learning_Management.Domain.Interfaces.Post;

namespace Online_Learning_Management.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]//api/posts
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDTO createPostDTO)
        {
            await _postService.AddPostAsync(createPostDTO);
            return Ok();
        }

        [HttpGet]//api/posts
        public async Task<IActionResult> GetAllPosts()
        {
            try {
                var posts = await _postService.GetAllPostsAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving posts", details = ex.Message });
            }
        }

        [HttpGet("{id}")]//api/posts/{id}
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            return Ok(post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] UpdatePostDTO updatePostDTO)
        {
            await _postService.UpdatePostAsync(id, updatePostDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            await _postService.DeletePostAsync(id);
            return Ok();
        }
    }
}
