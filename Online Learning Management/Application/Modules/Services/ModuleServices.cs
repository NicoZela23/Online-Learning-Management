using AutoMapper;
using Online_Learning_Management.Domain.Entities.Modules;
using Online_Learning_Management.Domain.Interfaces.Modules;
using Online_Learning_Management.Infrastructure.DTOs.Module;

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

        public async Task AddModuleAsync(CreateModuleDTO createModuleDTO)
        {
            var module = _mapper.Map<Module>(createModuleDTO);
            await _moduleRepository.AddModuleAsync(module);
        }

        public async Task DeleteModuleAsync(Guid id)
        {
            var module = await _moduleRepository.GetModuleByIdAsync(id);
            if (module == null)
            {
                throw new ArgumentException("Module not found.");
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
                throw new ArgumentException("Module not found");
            }
            return _mapper.Map<Module>(selectedModule);
        }

        public async Task UpdateModuleAsync(Guid id, UpdateModuleDTO moduleDto)
        {
            var existingModule = await _moduleRepository.GetModuleByIdAsync(id);

            if (existingModule == null)
            {
                throw new ArgumentException("Module not found.");
            }

            _mapper.Map(moduleDto, existingModule);

            await _moduleRepository.UpdateModuleAsync(existingModule);
        }
    }
}
