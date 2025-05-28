using AutoMapper;
using ExamRegistrationSystem.Dto;
using ExamRegistrationSystem.Entities;

namespace ExamRegistrationSystem.Mapping;

public class CustomMapping : Profile
{
    public CustomMapping()
    {
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Student, StudentDto>().ReverseMap();

        CreateMap<Exam, ExamDto>()
    .ForMember(dest => dest.ExamId, opt => opt.MapFrom(src => src.Id));

        CreateMap<ExamDto, Exam>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExamId));
    }
}
