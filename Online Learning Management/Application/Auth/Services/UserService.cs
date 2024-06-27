using AutoMapper;
using Online_Learning_Management.Application.Auth.Validator;
using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Domain.Exceptions.Auth;
using Online_Learning_Management.Domain.Interfaces.Auth;
using Online_Learning_Management.Domain.Interfaces.Instructors;
using Online_Learning_Management.Domain.Interfaces.Students;
using Online_Learning_Management.Infrastructure.DTOs.Auth;
using Online_Learning_Management.Infrastructure.DTOs.Instructor;
using Online_Learning_Management.Infrastructure.DTOs.Student;
using Online_Learning_Management.Infrastructure.Students;

namespace Online_Learning_Management.Application.Auth.Services
{
    public class UserService : IAuthUserService
    {
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IMapper _mapper;
        private readonly IStudentServices _studentServices;
        private readonly INstructorService _instructorService;
        public UserService(IUserAuthRepository userAuthRepository, IMapper mapper, IStudentServices studentServices, INstructorService instructorService)
        {
            _userAuthRepository = userAuthRepository;
            _mapper = mapper;
            _studentServices = studentServices;
            _instructorService = instructorService;
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

            if (createdUser.Role == "Student")
            {
                var createdStudent = _mapper.Map<CreateStudentDTO>(createdUser);
                await _studentServices.AddStudentAsync(createdStudent);
            }
            else if (createdUser.Role == "Instructor")
            {
                var createdInstructor = _mapper.Map<CreateInstructorDTO>(createdUser);
                await _instructorService.AddInstructorAsync(createdInstructor);
            }
            return createdUser;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userAuthRepository.GetUserByIdAsync(id);
            if(user == null)
            {
                throw new ArgumentException();
            }
            if (user.Role == "Student")
            {
                await _studentServices.DeleteStudentAsync(id);
            }
            else if (user.Role == "Instructor")
            {
                await _instructorService.DeleteInstructorAsync(id);
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

            if (!validationResult.IsValid) 
            { 
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new UserValidationException(errorMessages);
            }
            _mapper.Map(updateUserDTO, existingUser);
            var updatedUser = await _userAuthRepository.UpdateUserAsync(existingUser);

            if(existingUser.Role == "Student") 
            {
                var existingStudent = await _studentServices.GetStudentByIdAsync(id);
                var updatedStudent = _mapper.Map<UpdateStudentDTO>(updateUserDTO);

                if(existingStudent == null)
                {
                    throw new ArgumentException();
                }
                _mapper.Map(updatedStudent, existingStudent);
                await _studentServices.UpdateStudentAsync(id, updatedStudent);
            }
            else if (existingUser.Role == "Instructor")
            {
                var existingInstructor = await _instructorService.GetInstructorByIdAsync(id);
                var updatedInstructor = _mapper.Map<UpdateInstructorDTO>(updateUserDTO);

                if(existingInstructor == null)
                {
                    throw new ArgumentException();
                }
                _mapper.Map(updatedInstructor, existingInstructor);
                await _instructorService.UpdateInstructorAsync(id, updatedInstructor);
            }
            return updatedUser;
        }
    }
}
