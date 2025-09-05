using MediatR;
using OnlineCoursesPlatform.Application.Features.Lessons.Dto;

namespace OnlineCoursesPlatform.Application.Features.Lessons.Queries
{
    public record GetLessonsByCourseIdQuery(int CourseId) : IRequest<List<LessonListDto>>;
}
