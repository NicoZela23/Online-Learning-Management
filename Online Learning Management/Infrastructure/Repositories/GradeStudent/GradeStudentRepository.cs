using Online_Learning_Management.Domain.Interfaces.GradeStudent;
using Online_Learning_Management.Domain.Entities.GradeStudents;
using Online_Learning_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;



namespace Online_Learning_Management.Infrastructure.Repositories.GradeStudent
{
    public class GradeStudentRepository : IGradeStudentRepository
    {
        private readonly  ApplicationDbContext _context;
        public GradeStudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddGradeAsync(GradeStudents grade)
        {
            await _context.GradeStudents.AddAsync(grade);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteGradeAsync(Guid id)
        {
            var grade = await _context.GradeStudents.FindAsync(id);
            if (grade != null)
            {
                _context.GradeStudents.Remove(grade);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<GradeStudents>> GetGradeStudentByCourseId(Guid courseId)
        {
            return await _context.GradeStudents
                .Where(x => x.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<GradeStudents> GetGradeStudentById(Guid id)
        {
            return await _context.GradeStudents.FindAsync(id);
        }

        public async Task<IEnumerable<GradeStudents>> GetGradeStudentByStudentId(Guid studentId)
        {
            return await _context.GradeStudents
                .Where(x => x.StudentId == studentId)
                .ToListAsync();
        }

        public Task<IEnumerable<GradeStudents>> GetGradeStudentByStudentIdAndCourseId(Guid studentId, Guid courseId)
        {
            return Task.FromResult(_context.GradeStudents
                               .Where(x => x.StudentId == studentId && x.CourseId == courseId)
                                              .AsEnumerable());
        }

        public async Task UpdateGradeAsync(GradeStudents grade)
        {
            _context.GradeStudents.Update(grade);
            await _context.SaveChangesAsync();
        }
    }
}
