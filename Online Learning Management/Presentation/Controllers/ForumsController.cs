﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Application.Modules.Services;
using Online_Learning_Management.Domain.Entities.Forums;
using Online_Learning_Management.Domain.Interfaces.Forums;
using Online_Learning_Management.Domain.Interfaces.Modules;
using Online_Learning_Management.Infrastructure.DTOs.Forum;


namespace Online_Learning_Management.Presentation.Controllers
{
    [Route("api/courses/forums")]
    [ApiController]
    public class ForumsController : ControllerBase
    {

        private readonly IForumService _forumService;
        public ForumsController(IForumService forumService)
        {
            _forumService = forumService;
        }

        [HttpGet]//Get  localhost:5000/course/forums
        [Authorize]
        public async Task<ActionResult<IEnumerable<Forum>>> GetAllForums()
        {
            try
            {
                var forums = await _forumService.GetAllForumsAsync();
                return Ok(forums);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Forum>> GetForumById(Guid id)
        {
            try
            {
                var forum = await _forumService.GetForumByIdAsync(id);

                if (forum == null)
                {
                    return NotFound();
                }

                return Ok(forum);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddForum(CreateForumDTO forumDto)
        {
            try
            {
                await _forumService.AddForumAsync(forumDto);
                return StatusCode(201, "Forum successfully added.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateForum(Guid id, [FromBody] UpdateForumDTO forumDto)
        {
            try
            {
                await _forumService.UpdateForumAsync(id, forumDto);
                return Ok("Forum successfully updated.");
            }
            catch (ArgumentException)
            {
                return NotFound("Forum not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteForum(Guid id)
        {
            try
            {
                await _forumService.DeleteForumAsync(id);
                 return Ok(new { message = "Forum was deleted succesfully" });
            }
            catch (ArgumentException)
            {
                return NotFound("Forum not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
