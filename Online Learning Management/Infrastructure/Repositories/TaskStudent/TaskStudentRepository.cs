using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Domain.Entities.TaskStudents;
using Online_Learning_Management.Domain.Interfaces.TaskStudents;
using Online_Learning_Management.Infrastructure.Data;

namespace Online_Learning_Management.Infrastructure.Repositories.TaskStudents
{
    public class TaskStudentRepository : ITaskStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskStudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TaskStudent> UploadTaskAsync(TaskStudent taskStudent)
        {
            var result = await _context.TaskStudents.AddAsync(taskStudent);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
