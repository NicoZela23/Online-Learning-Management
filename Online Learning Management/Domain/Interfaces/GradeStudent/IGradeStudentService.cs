﻿
using Online_Learning_Management.Domain.Entities.GradeStudents;
using Online_Learning_Management.Infrastructure.DTOs.GradeStudent;

namespace Online_Learning_Management.Domain.Interfaces.GradeStudent
{
    public interface IGradeStudentService
    {
        Task<GradeStudents> GetGradeStudentByIdAsync(Guid id);
        Task<IEnumerable<GradeStudents>> GetGradeStudentByStudentIdAsync(Guid studentId);
        Task<IEnumerable<GradeStudents>> GetGradeStudentByCourseIdAsync(Guid courseId);
        Task<IEnumerable<GradeStudents>> GetGradeStudentByStudentIdAndCourseIdAsync(Guid studentId, Guid courseId);
        Task AddGradeAsync(CreateGradeStudentDTO grade);
        Task UpdateGradeAsync(Guid id, UpdateGradeStudentDTO grade);
        Task DeleteGradeAsync(Guid id);
    }
}
