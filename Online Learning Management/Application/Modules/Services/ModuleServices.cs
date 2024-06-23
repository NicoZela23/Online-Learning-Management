using AutoMapper;
using FluentValidation;
using Online_Learning_Management.Application.Modules.Validator;
using Online_Learning_Management.Domain.Entities.Modules;
using Online_Learning_Management.Domain.Exceptions.Module;
using Online_Learning_Management.Domain.Interfaces.Modules;
using Online_Learning_Management.Infrastructure.DTOs.Module;
using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Application.Modules.Services
{
    public class ModuleServices : IModuleService
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;

        public ModuleServices(IModuleRepository moduleRepository, IMapper mapper)
        {
            _moduleRepository = moduleRepository;
            _mapper = mapper;
            
        }

        public async Task <Module> AddModuleAsync(CreateModuleDTO createModuleDTO)
        {
            var validator = new CreateModuleValidator();
            var validationResult = await validator.ValidateAsync(createModuleDTO);
            
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errorMessages);
            }

            var module = _mapper.Map<Module>(createModuleDTO);
            var createdModule = await _moduleRepository.AddModuleAsync(module);
            return createdModule;
        }

        public async Task DeleteModuleAsync(Guid id)
        {
            var module = await _moduleRepository.GetModuleByIdAsync(id);
            if (module == null)
            {
                throw new ModuleNotFoundException();
            }
            await _moduleRepository.DeleteModuleAsync(id);
        }

        public async Task<IEnumerable<Module>>GetAllModulesAsync()
        {
            var modules = await _moduleRepository.GetAllModulesAsync();
            return _mapper.Map<IEnumerable<Module>>(modules);
        }

        public async Task <Module> GetModuleByIdAsync(Guid id)
        {
            var selectedModule = await _moduleRepository.GetModuleByIdAsync(id);
            if (selectedModule == null)
            {
                throw new ModuleNotFoundException();
            }
            return _mapper.Map<Module>(selectedModule);
        }

        public async Task <Module> UpdateModuleAsync(Guid id, UpdateModuleDTO updateModuleDto)
        {
            var existingModule = await _moduleRepository.GetModuleByIdAsync(id);

            if (existingModule == null)
            {
                throw new ModuleNotFoundException();
            }

            var validator = new UpdateModuleValidator();
            var validationResult = await validator.ValidateAsync(updateModuleDto);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ModuleValidationException(errorMessages);
            }

            _mapper.Map(updateModuleDto, existingModule);
            var updatedModule = await _moduleRepository.UpdateModuleAsync(existingModule);
            return updatedModule;
        }
    }
}
