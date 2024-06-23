using Microsoft.IdentityModel.Tokens;
using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Domain.Exceptions.Auth;
using Online_Learning_Management.Domain.Interfaces.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Online_Learning_Management.Application.Auth.Services
{
    public class AuthService : IAuthValidService
    {
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserAuthRepository userAuthRepository, IConfiguration configuration)
        {
            _userAuthRepository = userAuthRepository;
            _configuration = configuration;
        }
        public async Task <User> Authenticate(UserLogin userlogin)
        {
            var userToAuthenticate = await _userAuthRepository.GetUserByLoginCredentials(userlogin);

            if(userToAuthenticate == null)
            {
                throw new UserNotFoundException();
            }
            return userToAuthenticate;
        }

        public string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
