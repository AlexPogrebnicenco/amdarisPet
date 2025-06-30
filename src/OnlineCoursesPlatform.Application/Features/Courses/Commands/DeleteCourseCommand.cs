using MediatR;

namespace OnlineCoursesPlatform.Application.Features.Courses.Commands
{
    public record DeleteCourseCommand(int Id) : IRequest<Unit>;
}
