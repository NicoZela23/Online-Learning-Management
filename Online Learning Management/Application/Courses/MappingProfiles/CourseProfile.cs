using AutoMapper;
using Online_Learning_Management.Domain.Entities.Courses;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<CreateCourseDTO, Course>();
        CreateMap<UpdateCourseDTO, Course>();
    }
}