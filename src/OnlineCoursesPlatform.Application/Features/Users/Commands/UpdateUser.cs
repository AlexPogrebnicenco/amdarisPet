using MediatR;
using OnlineCoursesPlatform.Application.Features.Users.Dto;

namespace OnlineCoursesPlatform.Application.Features.Users.Commands
{
    public record UpdateUser(int UserId, UpdateUserDto Dto) : IRequest<UserAccountInfoDto>;
}
