using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Application.Auth.Responses;
using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Domain.Exceptions.Auth;
using Online_Learning_Management.Domain.Interfaces.Auth;

namespace Online_Learning_Management.Presentation.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthValidService _authValidService;

        public LoginController(IAuthValidService authValidService)
        {
            _authValidService = authValidService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            try 
            {
                var user = await _authValidService.Authenticate(userLogin);
                var token = _authValidService.Generate(user);
                return Ok(token);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
