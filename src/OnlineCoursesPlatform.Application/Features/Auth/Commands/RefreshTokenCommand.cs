using MediatR;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands
{
    public record RefreshTokenCommand(RefreshTokenRequestDto Dto) : IRequest<AuthResultDto>;
}
