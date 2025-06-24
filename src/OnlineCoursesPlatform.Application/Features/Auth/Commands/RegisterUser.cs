using MediatR;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands
{
    public record class RegisterUser(RegisterDto Dto) : IRequest<AuthResponse>;
}
