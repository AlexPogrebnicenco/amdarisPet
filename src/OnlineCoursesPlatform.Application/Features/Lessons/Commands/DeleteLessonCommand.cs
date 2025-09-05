using MediatR;

namespace OnlineCoursesPlatform.Application.Features.Lessons.Commands
{
    public record DeleteLessonCommand(int CourseId, int OrderNumber) : IRequest<Unit>;
}
