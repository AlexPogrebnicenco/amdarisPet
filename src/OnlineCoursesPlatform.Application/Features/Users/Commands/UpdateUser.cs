using MediatR;
using OnlineCoursesPlatform.Application.Features.Users.Dto;

namespace OnlineCoursesPlatform.Application.Features.Users.Commands
{
    public record UpdateUser(int Id, UpdateUserDto Dto) : IRequest<UserDto?>;
}
