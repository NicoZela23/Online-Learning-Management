using AutoMapper;
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
            return _mapper.Map<Student>(student);
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return _mapper.Map<IEnumerable<Student>>(students);
        }

        public async Task AddStudentAsync(CreateStudentDTO createStudentDTO)
        {
            var student = _mapper.Map<Student>(createStudentDTO);
            student.CreateAt = DateOnly.FromDateTime(DateTime.Now);
            await _studentRepository.AddStudentAsync(student);
        }

        public async Task UpdateStudentAsync(Guid id, UpdateStudentDTO updateStudentDTO)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            student.Name = updateStudentDTO.Name;
            student.LastName = updateStudentDTO.LastName;
            student.Email = updateStudentDTO.Email;
            student.UpdateAt = DateOnly.FromDateTime(DateTime.Now);
            await _studentRepository.UpdateStudentAsync(student);
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            await _studentRepository.DeleteStudentAsync(id);
        }
    }
}
