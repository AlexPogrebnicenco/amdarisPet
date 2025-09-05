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
                .ForMember(dest => dest.CourseTags, opt => opt.Ignore())
                .ForMember(dest => dest.About, opt => opt.MapFrom(src =>
                     string.IsNullOrWhiteSpace(src.About) ? "Nothing about this course" : src.About));

            // Маппинг для обновления курса
            CreateMap<UpdateCourseDto, Course>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.DateModified, opt => opt.Ignore())
                .ForMember(dest => dest.CourseTags, opt => opt.Ignore())
                .ForMember(dest => dest.About, opt => opt.Condition(src => src.About != null));

            // Маппинг для возврата курса (Course → CourseDto)
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => src.Difficulty.ToString()))
                .ForMember(dest => dest.LessonsCount, opt => opt.MapFrom(src => src.Lessons.Count))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null))
                .ForMember(dest => dest.AuthorAvatarUrl, opt => opt.MapFrom(src =>
                    src.CourseAuthors
                        .Where(ca => ca.User != null && ca.User.AvatarUrl != null)
                        .Select(ca => ca.User!.AvatarUrl)
                        .FirstOrDefault()
                ))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src =>
                    src.CourseAuthors.Select(ca => ca.UserId).FirstOrDefault()
                ))
                .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.About));
        }
    }
}
