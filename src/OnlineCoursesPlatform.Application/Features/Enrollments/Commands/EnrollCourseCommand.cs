using MediatR;

namespace OnlineCoursesPlatform.Application.Features.Enrollments.Commands
{
    public record EnrollCourseCommand(int UserId, int CourseId) : IRequest<Unit>;

}
