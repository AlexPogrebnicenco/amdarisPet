using MediatR;

namespace OnlineCoursesPlatform.Application.Features.Users.Commands
{
    public record DeleteUser(int Id) : IRequest<bool>;
}