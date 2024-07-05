using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Domain.Entities.TaskStudents;
using Online_Learning_Management.Domain.Interfaces.TaskStudents;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.DTOs.TaskStudent;

namespace Online_Learning_Management.Infrastructure.Repositories.TaskStudents
{
    public class TaskStudentRepository : ITaskStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskStudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Object>> GetAllSubmittedTasksAsync(Guid taskID)
        {
            var result = await(from ts in _context.TaskStudents
                               join s in _context.Students on ts.StudentID equals s.Id
                               join uf in _context.UploadedFiles on ts.FileID equals uf.Id
                               where ts.ModuleTaskID == taskID
                               select new
                               {
                                   id = ts.Id,
                                   StudentName = s.Name,
                                   TaskUpload = ts.UploadDate,
                                   TaskFileURL = uf.BlobURL,
                                   TaskQualification = ts.Qualification,
                                   TaskComment = ts.Comment
                               }).ToListAsync();

            return result;
        }

        public async Task<Object> GetRelationshipDataFromTaaskStudents(Guid taskID)
        {
            var result = await(from ts in _context.TaskStudents
                               join mt in _context.ModuleTasks on ts.ModuleTaskID equals mt.Id
                               join m in _context.Modules on mt.ModuleID equals m.Id
                               join c in _context.Courses on m.CourseID equals c.Id
                               where ts.ModuleTaskID == taskID
                               select new
                               {
                                   taskId = taskID,
                                   CourseName = c.Title,
                                   ModuleName = m.Name,
                                   TaskTitle = mt.Title,
                                   TaskDeadline = mt.Deadline
                               }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<object> GetSubmittedTaskByIdAsync(Guid studentTaskId)
        {
            var result = await(from ts in _context.TaskStudents
                               join s in _context.Students on ts.StudentID equals s.Id
                               join uf in _context.UploadedFiles on ts.FileID equals uf.Id
                               join mt in _context.ModuleTasks on ts.ModuleTaskID equals mt.Id
                               where ts.Id == studentTaskId
                               select new
                               {
                                   id = ts.Id,
                                   StudentName = s.Name,
                                   TaskTitle = mt.Title,
                                   TaskDeadline = mt.Deadline,
                                   TaskUpload = ts.UploadDate,
                                   TaskFileURL = uf.BlobURL,
                                   TaskQualification = ts.Qualification,
                                   TaskComment = ts.Comment
                               }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<TaskStudent> UpdateTaskStudentAsync(Guid studentTaskId, UpdateTaskStudentDTO taskStudentDto)
        {
            var existingTaskStudent = await _context.TaskStudents
               .FirstOrDefaultAsync(ts => ts.Id == studentTaskId);

            existingTaskStudent.Qualification = taskStudentDto.Qualification;
            existingTaskStudent.Comment = taskStudentDto.Comment;

            _context.TaskStudents.Update(existingTaskStudent);

            await _context.SaveChangesAsync();
            return existingTaskStudent;
        }

        public async Task<TaskStudent> UploadTaskAsync(TaskStudent taskStudent)
        {
            var result = await _context.TaskStudents.AddAsync(taskStudent);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
