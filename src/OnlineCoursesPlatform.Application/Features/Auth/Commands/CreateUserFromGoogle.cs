using MediatR;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands
{
    public record CreateUserFromGoogle(CreateUserFromGoogleDto Dto) : IRequest<AuthResponse>;
}
