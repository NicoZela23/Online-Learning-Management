using AutoMapper;
using Online_Learning_Management.Application.TaskStudent.Validator;
using TaskStudentEntity = Online_Learning_Management.Domain.Entities.TaskStudents.TaskStudent;
using Online_Learning_Management.Domain.Interfaces.TaskStudents;
using Online_Learning_Management.Infrastructure.DTOs.TaskStudent;
using Online_Learning_Management.Domain.Interfaces.ModuleTasks;
using Online_Learning_Management.Domain.Interfaces.Students;
using System.Threading.Tasks;
using Online_Learning_Management.Application.TaskStudent.Responses;
using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Domain.Interfaces.Files;

namespace Online_Learning_Management.Application.TaskStudent.Services
{
    public class TaskStudentService : ITaskStudentService
    {
        private readonly IMapper _mapper;
        private readonly ITaskStudentRepository _taskStudentRepository;
        private readonly IModuleTaskRepository _moduleTaskRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IFileRepository _fileRepository;

        public TaskStudentService(
            ITaskStudentRepository taskStudentRepository,
            IModuleTaskRepository moduleTaskRepository,
            IStudentRepository studentRepository,
            IFileRepository fileRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _taskStudentRepository = taskStudentRepository;
            _moduleTaskRepository = moduleTaskRepository;
            _studentRepository = studentRepository;
            _fileRepository = fileRepository;
        }

        public async Task<TaskStudentEntity> UploadTaskAsync(Guid moduleTaskId, Guid studentId, Guid fileId)
        {
            var moduleTask = await _moduleTaskRepository.GetTaskOfModuleByIdAsync(moduleTaskId);
            var student = await _studentRepository.GetStudentByIdAsync(studentId);

            if (moduleTask == null) throw new Exception("The task does not exist.");
            if (student == null) throw new Exception("The student does not exist.");

            var taskStudentDto = new CreateTaskStudentDTO
            {
                ModuleTaskID = moduleTaskId,
                StudentID = studentId,
                FileID = fileId
            };

            var validator = new CreateTaskStudentValidator();
            var validate = await validator.ValidateAsync(taskStudentDto);

            if (!validate.IsValid)
            {
                var errorMessages = string.Join("; ", validate.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errorMessages);
            }

            var taskStudentMap = _mapper.Map<TaskStudentEntity>(taskStudentDto);
            var createdTask = await _taskStudentRepository.UploadTaskAsync(taskStudentMap);

            return createdTask;
        }

        public async Task<RelationshipDataStructure>  StructureResponse(
            TaskStudentEntity taskStudent, 
            Guid taskID, 
            Guid studentID,
            Guid fileID
        )
        {
            var moduleTask = await _moduleTaskRepository.GetTaskOfModuleByIdAsync(taskID);
            var student = await _studentRepository.GetStudentByIdAsync(studentID);
            var file = await _fileRepository.GetFileDataByIdAsync(fileID);

            var response = new RelationshipDataStructure
            {
                TaskTitle = moduleTask.Title,
                StudentName = student.Name,
                FileUrl = file.BlobURL
            };

            return response;
        }
    }
}
