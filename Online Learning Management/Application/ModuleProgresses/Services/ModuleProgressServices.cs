using AutoMapper;
using Online_Learning_Management.Application.ModuleProgresses.Validator;
using Online_Learning_Management.Domain.Entities.ModuleProgresses;
using Online_Learning_Management.Domain.Exceptions.ModuleProgress;
using Online_Learning_Management.Infrastructure.DTOs.ModuleProgresses;
using OnlineLearningManagement.Domain.Exceptions.ModuleProgress;
using OnlineLearningManagement.Domain.Interfaces;


namespace Online_Learning_Management.Application.Modules.Services.ModuleProgresses
{
    public class ModuleProgressServices : IModuleProgressService
    {
        private readonly IModuleProgressRepository _moduleProgressRepository;
        private readonly IMapper _mapper;

        public ModuleProgressServices(IModuleProgressRepository moduleProgressRepository, IMapper mapper)
        {
            _moduleProgressRepository = moduleProgressRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ModuleProgress>> GetAllModuleProgressesAsync()
        {
            var moduleProgresses = await _moduleProgressRepository.GetAllModuleProgressesAsync();
            return moduleProgresses;
        }

        public async Task<ModuleProgress> GetModuleProgressByIdAsync(Guid id)
        {
            var moduleProgress = await _moduleProgressRepository.GetModuleProgressByIdAsync(id);
            if (moduleProgress == null)
            {
                throw new ModuleProgressNotFoundException();
            }
            return moduleProgress;
        }

        public async Task<ModuleProgress> AddModuleProgressAsync(CreateModuleProgressDTO createModuleProgressDTO)
        {
            if (await _moduleProgressRepository.ModuleProgressExistsAsync(createModuleProgressDTO.CourseId, createModuleProgressDTO.ModuleId, createModuleProgressDTO.StudentId))
            {
                throw new ArgumentException("ModuleProgress with the same CourseId, ModuleId, and StudentId already exists");
            }

            await ValidateModuleProgressData(createModuleProgressDTO.CourseId, createModuleProgressDTO.ModuleId, createModuleProgressDTO.StudentId);

            var validator = new CreateModuleProgressValidator();
            var validationResult = await validator.ValidateAsync(createModuleProgressDTO);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errorMessages);
            }

            var moduleProgress = _mapper.Map<ModuleProgress>(createModuleProgressDTO);
            var createdModuleProgress = await _moduleProgressRepository.AddModuleProgressAsync(moduleProgress);
            return createdModuleProgress;
        }

        public async Task<ModuleProgress> UpdateModuleProgressAsync(Guid id, UpdateModuleProgressDTO updateModuleProgressDTO)
        {
            var existingModuleProgress = await _moduleProgressRepository.GetModuleProgressByIdAsync(id);
            if (existingModuleProgress == null)
            {
                throw new ModuleProgressNotFoundException();
            }

            await ValidateModuleProgressData(updateModuleProgressDTO.CourseId, updateModuleProgressDTO.ModuleId, updateModuleProgressDTO.StudentId);

            var validator = new UpdateModuleProgressValidator();
            var validationResult = await validator.ValidateAsync(updateModuleProgressDTO);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ModuleProgressValidationException(errorMessages);
            }

            _mapper.Map(updateModuleProgressDTO, existingModuleProgress);
            var updatedModuleProgress = await _moduleProgressRepository.UpdateModuleProgressAsync(existingModuleProgress);
            return updatedModuleProgress;
        }

        public async Task<ModuleProgress> PatchModuleProgressAsync(Guid id, PatchModuleProgressDTO progress)
        {
            var existingModuleProgress = await _moduleProgressRepository.GetModuleProgressByIdAsync(id);
            if (existingModuleProgress == null)
            {
                throw new ModuleProgressNotFoundException();
            }

            var validator = new PatchModuleProgressValidator();
            var validationResult = await validator.ValidateAsync(progress);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException($"Validation failed: {errorMessages}");
            }

            var updatedModuleProgress = await _moduleProgressRepository.PatchModuleProgressAsync(id, progress.Progress);
            return updatedModuleProgress;
        }

        public async Task DeleteModuleProgressAsync(Guid id)
        {
            var moduleProgress = await _moduleProgressRepository.GetModuleProgressByIdAsync(id);
            if (moduleProgress == null)
            {
                throw new ModuleProgressNotFoundException();
            }
            await _moduleProgressRepository.DeleteModuleProgressAsync(id);
        }

        public async Task<bool> CourseExists(Guid courseId)
        {
            var courseExists = await _moduleProgressRepository.CourseExistsAsync(courseId);
            if (!courseExists)
            {
                throw new ArgumentException("CourseId provided does not exist");
            }
            return courseExists;
        }

        public async Task<bool> ModuleExists(Guid moduleId)
        {
            var moduleExists = await _moduleProgressRepository.ModuleExistsAsync(moduleId);
            if (!moduleExists)
            {
                throw new ArgumentException("ModuleId provided does not exist");
            }
            return moduleExists;
        }

        public async Task<bool> StudentExists(Guid studentId)
        {
            var studentExists = await _moduleProgressRepository.StudentExistsAsync(studentId);
            if (!studentExists)
            {
                throw new ArgumentException("StudentId provided does not exist");
            }
            return studentExists;
        }

        private async Task ValidateModuleProgressData(Guid courseId, Guid moduleId, Guid studentId)
        {
            if (!await _moduleProgressRepository.CourseExistsAsync(courseId))
            {
                throw new ArgumentException("CourseId provided does not exist");
            }

            if (!await _moduleProgressRepository.ModuleExistsAsync(moduleId))
            {
                throw new ArgumentException("ModuleId provided does not exist");
            }

            if (!await _moduleProgressRepository.StudentExistsAsync(studentId))
            {
                throw new ArgumentException("StudentId provided does not exist");
            }
        }
    }
}
