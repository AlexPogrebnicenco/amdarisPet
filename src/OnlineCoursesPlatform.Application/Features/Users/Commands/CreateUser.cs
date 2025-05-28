using MediatR;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Application.Interfaces;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Services;

namespace OnlineCoursesPlatform.Application.Features.Users.Commands
{
    public record CreateUser(string Name, string Email) : IRequest<UserDto>;

    public class CreateUserHandler : IRequestHandler<CreateUser, UserDto>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<UserDto> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var user = new User() { UserName = request.Name, Email = request.Email, Id = GetNextId() };
            _userService.Add(user);
            return Task.FromResult(UserDto.FromUser(user));
        }

        private int GetNextId() 
        {
            return _userService.GetLastId();
        }
    }

}
