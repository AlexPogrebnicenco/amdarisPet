using MediatR;

namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Commands
{
    public record RejectTeacherRequestCommand(int RequestId) : IRequest<Unit>;
}
