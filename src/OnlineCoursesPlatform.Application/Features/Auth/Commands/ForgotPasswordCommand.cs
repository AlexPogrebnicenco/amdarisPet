using MediatR;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands
{
    public record ForgotPasswordCommand(string Email) : IRequest<Unit>;
}
