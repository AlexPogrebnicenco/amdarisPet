using MediatR;
using OnlineCoursesPlatform.Application.Features.Lessons.Dto;

namespace OnlineCoursesPlatform.Application.Features.Lessons.Queries
{
    public record GetLessonByOrderNumberQuery(int CourseId, int OrderNumber) : IRequest<LessonDto>;
}
