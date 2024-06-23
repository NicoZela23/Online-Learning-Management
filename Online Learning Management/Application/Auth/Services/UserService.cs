using AutoMapper;
using Online_Learning_Management.Application.Auth.Validator;
using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Domain.Exceptions.Auth;
using Online_Learning_Management.Domain.Interfaces.Auth;
using Online_Learning_Management.Infrastructure.DTOs.Auth;

namespace Online_Learning_Management.Application.Auth.Services
{
    public class UserService : IAuthUserService
    {
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IMapper _mapper;
        public UserService(IUserAuthRepository userAuthRepository, IMapper mapper)
        {
            _userAuthRepository = userAuthRepository;
            _mapper = mapper;
        }
    
        public async Task <User> AddUserAsync(CreateUserDTO createUserDTO)
        {
            var validator = new CreateUserValidator();
            var validationResult = await validator.ValidateAsync(createUserDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errorMessages);
            }
            var user = _mapper.Map<User>(createUserDTO);
            var createdUser = await _userAuthRepository.AddUserAsync(user);
            return createdUser;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userAuthRepository.GetUserByIdAsync(id);
            if(user == null)
            {
                throw new ArgumentException();
            }
            await _userAuthRepository.DeleteUserAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _userAuthRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<User>>(users);
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var selectedUser = await _userAuthRepository.GetUserByIdAsync(id);   
            if(selectedUser == null)
            {
                throw new ArgumentException();
            }
            return _mapper.Map<User>(selectedUser);
        }

        public async Task <User> UpdateUserAsync(Guid id, UpdateUserDTO updateUserDTO)
        {
            var existingUser = await _userAuthRepository.GetUserByIdAsync(id);

            if(existingUser == null)
            {
                throw new UserNotFoundException();
            }
            var validator = new UpdateUserValidator();
            var validationResult = await validator.ValidateAsync(updateUserDTO);

            if (validationResult.IsValid) 
            { 
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new UserValidationException(errorMessages);
            }
            _mapper.Map(updateUserDTO, existingUser);
            var updatedUser = await _userAuthRepository.UpdateUserAsync(existingUser);
            return updatedUser;
        }
    }
}
