﻿using Online_Learning_Management.Infrastructure.DTOs.ReportCourse;

namespace Online_Learning_Management.Domain.Interfaces.ReportCourses
{
    public interface IReportCourseService
    {
        Task<Object> GetReportCourseByStudentAndCourseAsync(Guid studentId, Guid courseId);
    }
}
