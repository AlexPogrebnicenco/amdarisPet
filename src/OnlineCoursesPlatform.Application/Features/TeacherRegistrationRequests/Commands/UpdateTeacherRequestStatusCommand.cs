using MediatR;

namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Commands
{
    public record UpdateTeacherRequestStatusCommand(int RequestId, string Status) : IRequest<Unit>;

}
