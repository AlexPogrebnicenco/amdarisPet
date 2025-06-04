using MediatR;
using OnlineCoursesPlatform.Application.Features.Users.Dto;

namespace OnlineCoursesPlatform.Application.Features.Users.Commands
{
    public record CreateUser(CreateUserDto Dto) : IRequest<UserDto>;
}
