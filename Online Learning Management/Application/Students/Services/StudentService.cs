using AutoMapper;
using Online_Learning_Management.Application.Students.Validator;
using Online_Learning_Management.Domain.Entities.Students;
using Online_Learning_Management.Domain.Interfaces.Students;
using Online_Learning_Management.Infrastructure.DTOs.Student;

namespace Online_Learning_Management.Application.Students.Services
{
    public class StudentService : IStudentServices
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;


        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Student> GetStudentByIdAsync(Guid id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                throw new ArgumentException("The student does not exist.");
            }
            return _mapper.Map<Student>(student);
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            if (students == null)
            {
                throw new ArgumentException("There are no students.");
            }
            return _mapper.Map<IEnumerable<Student>>(students);
        }

        public async Task<Student> AddStudentAsync(CreateStudentDTO createStudentDTO)
        {
            var validator = new CreateStudentValidator();
            var validate = await validator.ValidateAsync(createStudentDTO);
            if (!validate.IsValid) 
            {
                var errors = string.Join("; ", validate.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errors);
            }
            var student = _mapper.Map<Student>(createStudentDTO);
            student.CreateAt = DateTime.Now;
            var createdStudent = await _studentRepository.AddStudentAsync(student);
            return createdStudent;
        }

        public async Task UpdateStudentAsync(Guid id, UpdateStudentDTO updateStudentDTO)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                throw new ArgumentException("The student does not exist.");
            }
            student.Name = updateStudentDTO.Name;
            student.LastName = updateStudentDTO.LastName;
            student.Email = updateStudentDTO.Email;
            await _studentRepository.UpdateStudentAsync(student);
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                throw new ArgumentException("The student does not exist.");
            }
            await _studentRepository.DeleteStudentAsync(id);
        }
    }
}
