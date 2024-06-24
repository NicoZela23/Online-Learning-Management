using AutoMapper;
using Online_Learning_Management.Domain.Interfaces.ReportCourses;
using Online_Learning_Management.Infrastructure.DTOs.ReportCourse;

namespace Online_Learning_Management.Application.ReportCourses.Services
{
    public class ReportCourseService : IReportCourseService
    {
        private readonly IReportCourseRepository _reportCourseRepository;
        private readonly IMapper _mapper;

        public ReportCourseService(IReportCourseRepository reportCourseRepository, IMapper mapper)
        {
            _reportCourseRepository = reportCourseRepository;
            _mapper = mapper;
        }

        public async Task<ReportCourseDTO> GetReportCourseByIdAsync(Guid id)
        {
            var reportCourse = await _reportCourseRepository.GetReportCourseByIdAsync(id);
            if (reportCourse == null)
            {
                throw new KeyNotFoundException("Course not found");
            }
            return _mapper.Map<ReportCourseDTO>(reportCourse);
        }
    }
}
