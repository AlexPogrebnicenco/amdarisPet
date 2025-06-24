using MediatR;
using OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Dto;

namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Commands
{
    public record CreateTeacherRegistrationRequestCommand(CreateTeacherRegistrationRequestDto Dto) : IRequest<Unit>;
}
