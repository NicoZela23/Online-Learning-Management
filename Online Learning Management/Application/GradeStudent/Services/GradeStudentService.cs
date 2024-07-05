using AutoMapper;
using Online_Learning_Management.Domain.Entities.GradeStudents;
using Online_Learning_Management.Domain.Interfaces.GradeStudent;
using Online_Learning_Management.Infrastructure.DTOs.GradeStudent;
using Online_Learning_Management.Infrastructure.DTOs.Module;

namespace Online_Learning_Management.Application.GradeStudent.Services
{
    public class GradeStudentService : IGradeStudentService
    {
        private readonly IGradeStudentRepository _gradeStudentRepository;
        private readonly IMapper _mapper;
        public GradeStudentService(IGradeStudentRepository gradeStudentRepository, IMapper mapper)
        {
            _gradeStudentRepository = gradeStudentRepository;
            _mapper = mapper;
        }
        public async Task AddGradeAsync(CreateGradeStudentDTO createGradeStudentDTO)
        {
         var gradeStudent = _mapper.Map<GradeStudents>(createGradeStudentDTO);
            await _gradeStudentRepository.AddGradeAsync(gradeStudent);
        }

        public  async Task DeleteGradeAsync(Guid id)
        {
            var gradeStudent = await _gradeStudentRepository.GetGradeStudentById(id);
            if (gradeStudent == null)
            {
                throw new ArgumentException("GradeStudent not found.");
            }
            await _gradeStudentRepository.DeleteGradeAsync(id);
        }

        public async Task<IEnumerable<GradeStudents>> GetGradeStudentByCourseIdAsync(Guid courseId)
        {
            var gradeStudents = await _gradeStudentRepository.GetGradeStudentByCourseId(courseId);
            return _mapper.Map<IEnumerable<GradeStudents>>(gradeStudents);
        }

        public async Task<GradeStudents> GetGradeStudentByIdAsync(Guid id)
        {
            var selectedGradeStudent = await _gradeStudentRepository.GetGradeStudentById(id);
            if (selectedGradeStudent == null)
            {
                throw new ArgumentException("GradeStudent not found");
            }
            return _mapper.Map<GradeStudents>(selectedGradeStudent);
        }
       

        public async Task<IEnumerable<GradeStudents>> GetGradeStudentByStudentIdAndCourseIdAsync(Guid studentId, Guid courseId)
        {
            var gradeStudents = await _gradeStudentRepository.GetGradeStudentByStudentIdAndCourseId(studentId ,courseId);
            return _mapper.Map<IEnumerable<GradeStudents>>(gradeStudents);
        }

        public async Task<IEnumerable<GradeStudents>> GetGradeStudentByStudentIdAsync(Guid studentId)
        {
            var gradeStudents = await _gradeStudentRepository.GetGradeStudentByStudentId(studentId);
            return _mapper.Map<IEnumerable<GradeStudents>>(gradeStudents);
        }

        public async Task UpdateGradeAsync(Guid id, UpdateGradeStudentDTO gradeStudentDto)
        {
            var existingGradeStudent = await _gradeStudentRepository.GetGradeStudentById(id);

            if (existingGradeStudent == null)
            {
                throw new ArgumentException("GradeStudent not found.");
            }

            _mapper.Map(gradeStudentDto, existingGradeStudent);
            await _gradeStudentRepository.UpdateGradeAsync(existingGradeStudent);
        }

    }
}



