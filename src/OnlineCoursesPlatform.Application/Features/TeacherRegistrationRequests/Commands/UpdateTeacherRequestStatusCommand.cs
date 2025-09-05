using MediatR;
using OnlineCoursesPlatform.Domain.Enums;

namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Commands
{
    public record UpdateTeacherRequestStatusCommand(int RequestId, TeacherRequestStatus Status) : IRequest<Unit>;
}
