using AutoMapper;
using Online_Learning_Management.Domain.Entities;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<CreateCourseDTO, Course>();
    }
}