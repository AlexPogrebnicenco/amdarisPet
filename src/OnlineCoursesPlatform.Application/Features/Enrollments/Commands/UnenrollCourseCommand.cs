using MediatR;

namespace OnlineCoursesPlatform.Application.Features.Enrollments.Commands
{
    public record UnenrollCourseCommand(int UserId, int CourseId) : IRequest<Unit>;

}
