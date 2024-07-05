using AutoMapper;
using Online_Learning_Management.Application.ModuleTasks.Validator;
using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Domain.Interfaces.Modules;
using Online_Learning_Management.Domain.Interfaces.ModuleTasks;
using Online_Learning_Management.Infrastructure.DTOs.ModuleTask;

namespace Online_Learning_Management.Application.ModuleTasks.Services
{
    public class ModuleTaskService : IModuleTaskService
    {
        private readonly IModuleTaskRepository _moduleTaskRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;

        public ModuleTaskService(IModuleTaskRepository moduleTaskRepository, IModuleRepository moduleRepository, IMapper mapper)
        {
            _moduleTaskRepository = moduleTaskRepository;
            _moduleRepository = moduleRepository;
            _mapper = mapper;
        }

        public async Task<ModuleTask> AddTaskToModuleAsync(CreateModuleTaskDTO moduleTaskDto)
        {
            var validator = new CreateModuleTaskValidator();
            var validate = await validator.ValidateAsync(moduleTaskDto);

            if (!validate.IsValid)
            {
                var errorMessages = string.Join("; ", validate.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errorMessages);
            }

            var moduleTask = _mapper.Map<ModuleTask>(moduleTaskDto);
            var createdModuleTask = await _moduleTaskRepository.AddTaskToModuleAsync(moduleTask);

            return createdModuleTask;
        }

        public async Task DeleteTaskOfModuleAsync(Guid id)
        {
            var moduleTask = await _moduleTaskRepository.GetTaskOfModuleByIdAsync(id);
            if (moduleTask == null)
            {
                throw new ArgumentException("The task does not exist.");
            }
            await _moduleTaskRepository.DeleteTaskOfModuleAsync(id);
        }

        public async Task<IEnumerable<ModuleTask>> GetAllTasksOfModuleAsync(Guid moduleID)
        {
            var module = await _moduleRepository.GetModuleByIdAsync(moduleID);

            if (module == null)
            {
                throw new Exception("Module not found");
            }

            var moduleTasks = await _moduleTaskRepository.GetAllTasksOfModuleAsync(moduleID);
            return _mapper.Map<IEnumerable<ModuleTask>>(moduleTasks);
        }

        public async Task<ModuleTask> GetTaskOfModuleByIdAsync(Guid id)
        {
            var moduleTask = await _moduleTaskRepository.GetTaskOfModuleByIdAsync(id);
            if (moduleTask == null)
            {
                throw new ArgumentException("The task does not exist.");
            }
            return _mapper.Map<ModuleTask>(moduleTask);
        }

        public async Task<ModuleTask> UpdateTaskOfModuleAsync(Guid id, UpdateModuleTaskDTO moduleTaskDto)
        {
            var moduleTask = await _moduleTaskRepository.GetTaskOfModuleByIdAsync(id);

            if (moduleTask == null)
            {
                throw new ArgumentException("The task does not exist.");
            }

            _mapper.Map(moduleTaskDto, moduleTask);

            await _moduleTaskRepository.UpdateTaskOfModuleAsync(moduleTask);

            return moduleTask;
        }
    }
}
