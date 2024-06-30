using AutoMapper;
using Online_Learning_Management.Domain.Entities.Instructors;
using Online_Learning_Management.Domain.Exceptions.Auth;
using Online_Learning_Management.Domain.Interfaces.Instructors;
using Online_Learning_Management.Infrastructure.DTOs.Instructor;

namespace Online_Learning_Management.Application.Instructors.Services
{
    public class InstructorService : INstructorService
    {
        private readonly INstructorRepository _instructorRepository;
        private readonly IMapper _mapper;

        public InstructorService(INstructorRepository instructorRepository, IMapper mapper)
        {
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        public async Task AddInstructorAsync(CreateInstructorDTO createInstructorDTO)
        {
            var instructor = _mapper.Map<Instructor>(createInstructorDTO);
            await _instructorRepository.AddInstructorAsync(instructor);
        }

        public async Task DeleteInstructorAsync(Guid id)
        {
            var instructor = await _instructorRepository.GetInstructorByIdAsync(id);
            if(instructor == null)
            {
                throw new InstructorNotFoundException();
            }
            await _instructorRepository.DeleteInstructorAsync(id);
        }

        public async Task<IEnumerable<Instructor>> GetAllInstructorsAsync()
        {
            var instructors = await _instructorRepository.GetAllInstructorsAsync();
            if(instructors == null)
            {
                throw new ArgumentException("There are no instructors.");
            }
            return _mapper.Map<IEnumerable<Instructor>>(instructors);
        }

        public async Task<Instructor> GetInstructorByIdAsync(Guid id)
        {
            var instructor = await _instructorRepository.GetInstructorByIdAsync(id);
            if(instructor == null)
            {
                throw new InstructorNotFoundException();
            }
            return _mapper.Map<Instructor>(instructor);
        }

        public async Task UpdateInstructorAsync(Guid id, UpdateInstructorDTO updateInstructorDTO)
        {
            var existingInstructor = await _instructorRepository.GetInstructorByIdAsync(id);
            if(existingInstructor == null)
            {
                throw new InstructorNotFoundException();
            }
            _mapper.Map(updateInstructorDTO, existingInstructor);
            await _instructorRepository.UpdateInstructorAsync(existingInstructor);

        }
    }
}
