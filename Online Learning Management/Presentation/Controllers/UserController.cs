using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Application.Auth.Responses;
using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Domain.Exceptions.Auth;
using Online_Learning_Management.Domain.Interfaces.Auth;
using Online_Learning_Management.Infrastructure.DTOs.Auth;

namespace Online_Learning_Management.Presentation.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthUserService _userService;
        public UserController(IAuthUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddUser(CreateUserDTO userDto)
        {
            try
            {
                var createdUser= await _userService.AddUserAsync(userDto);
                var response = new ApiResponse("User created successfully", createdUser);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDTO userDto)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(id, userDto);
                var response = new ApiResponse("User updated successfully", updatedUser);
                return Ok(response);
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(UserValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
