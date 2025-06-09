using MediatR;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;
using OnlineCoursesPlatform.Application.Features.Users.Dto;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands
{
    public record class RegisterUser(RegisterDto Dto) : IRequest<UserDto>;
}
