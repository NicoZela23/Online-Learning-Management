using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Infrastructure.DTOs.Post;
using Online_Learning_Management.Domain.Interfaces.Post;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Online_Learning_Management.Presentation.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDTO createPostDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _postService.AddPostAsync(createPostDTO);
                return Ok(new { message = "Post created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the post", details = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                var posts = await _postService.GetAllPostsAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving posts", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid post ID.");

            try
            {
                var post = await _postService.GetPostByIdAsync(id);
                if (post == null)
                    return NotFound(new { message = "Post not found" });

                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the post", details = ex.Message });
            }
        }

        [HttpPut("{id}")]// api/posts/{id}
        [Authorize]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] UpdatePostDTO updatePostDTO)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid post ID.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _postService.UpdatePostAsync(id, updatePostDTO);
                return Ok(new { message = "Post updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the post", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid post ID.");

            try
            {
                await _postService.DeletePostAsync(id);
                return Ok(new { message = "Post deleted successfully" });
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            
        }
    }
}
