using MediatR;
using OnlineCoursesPlatform.Application.Features.Users.Dto;

namespace OnlineCoursesPlatform.Application.Features.Users.Queries
{
    public record GetUserById(int Id) : IRequest<UserDto?>;
}