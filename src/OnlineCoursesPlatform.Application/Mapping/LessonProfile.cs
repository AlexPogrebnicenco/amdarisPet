using AutoMapper;
using OnlineCoursesPlatform.Application.Features.Lessons.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Mapping
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            // VideoInfo <-> VideoInfoDto
            CreateMap<VideoInfoDto, VideoInfo>();
            CreateMap<VideoInfo, VideoInfoDto>();

            // CreateLessonDto -> Lesson
            CreateMap<CreateLessonDto, Lesson>();

            // UpdateLessonDto -> Lesson
            CreateMap<UpdateLessonDto, Lesson>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CourseId, opt => opt.Ignore());

            // Lesson -> UpdateLessonDto (for PATCH)
            CreateMap<Lesson, UpdateLessonDto>();

            // Lesson -> LessonDto, LessonListDto
            CreateMap<Lesson, LessonDto>();
            CreateMap<Lesson, LessonListDto>();
        }
    }
}
