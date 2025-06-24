using MediatR;

namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Commands
{
    public record ApproveTeacherRequestCommand(int RequestId) : IRequest<Unit>;
}
