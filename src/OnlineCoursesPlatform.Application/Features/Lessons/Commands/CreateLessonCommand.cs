using MediatR;
using OnlineCoursesPlatform.Application.Features.Lessons.Dto;

namespace OnlineCoursesPlatform.Application.Features.Lessons.Commands
{
    public record CreateLessonCommand(int CourseId, CreateLessonDto Dto) : IRequest<int>;
}
