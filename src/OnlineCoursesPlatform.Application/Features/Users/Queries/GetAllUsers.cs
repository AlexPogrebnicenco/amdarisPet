using MediatR;
using OnlineCoursesPlatform.Application.Features.Users.Dto;

namespace OnlineCoursesPlatform.Application.Features.Users.Queries
{
    public record GetAllUsers(int pageNumber = 1, int pageSize = 10) : IRequest<IEnumerable<UserDto>>;
}
