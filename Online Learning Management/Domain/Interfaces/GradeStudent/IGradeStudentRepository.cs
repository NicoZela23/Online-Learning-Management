using Online_Learning_Management.Domain.Entities.GradeStudents;

namespace Online_Learning_Management.Domain.Interfaces.GradeStudent
{
    public interface IGradeStudentRepository
    {
        Task<GradeStudents> GetGradeStudentById(Guid id); 
        Task<IEnumerable<GradeStudents>> GetGradeStudentByStudentId(Guid studentId); 
        Task<IEnumerable<GradeStudents>> GetGradeStudentByCourseId(Guid courseId); 
        Task<IEnumerable<GradeStudents>> GetGradeStudentByStudentIdAndCourseId(Guid studentId, Guid courseId); 
        Task AddGradeAsync(GradeStudents grade); 
        Task UpdateGradeAsync(GradeStudents grade); 
        Task DeleteGradeAsync(Guid id);
    }
}
