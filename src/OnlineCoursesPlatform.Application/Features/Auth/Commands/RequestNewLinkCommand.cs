using MediatR;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands
{
    public record RequestNewLinkCommand(string Email) : IRequest<Unit>;
}
