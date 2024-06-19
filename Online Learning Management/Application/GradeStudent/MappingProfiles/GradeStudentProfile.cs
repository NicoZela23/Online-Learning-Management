using AutoMapper;
using Online_Learning_Management.Domain.Entities.GradeStudents;
using Online_Learning_Management.Infrastructure.DTOs.GradeStudent;


namespace Online_Learning_Management.Application.GradeStudent.MappingProfiles
{
    public class GradeStudentProfile: Profile
    {
        public GradeStudentProfile()
        {
            CreateMap<CreateGradeStudentDTO, GradeStudents>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); ;
            CreateMap<UpdateGradeStudentDTO, GradeStudents>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CourseId, opt => opt.Ignore());

        }
    }
}
