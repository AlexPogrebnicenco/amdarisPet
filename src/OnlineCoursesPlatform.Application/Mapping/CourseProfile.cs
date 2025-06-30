using AutoMapper;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Mapping
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            // Маппинг для создания курса
            CreateMap<CreateCourseDto, Course>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.DateModified, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.CourseTags, opt => opt.Ignore());

            // Маппинг для обновления курса
            CreateMap<UpdateCourseDto, Course>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.DateModified, opt => opt.Ignore());

            // Маппинг для возврата курса (Course → CourseDto)
            CreateMap<Course, CourseDto>();
        }
    }
}
