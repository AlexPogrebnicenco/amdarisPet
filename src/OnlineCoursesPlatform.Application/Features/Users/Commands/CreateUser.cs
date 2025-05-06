using MediatR;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Application.Interfaces;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Users.Commands
{
    public record CreateUser(string Name, string Email) : IRequest<UserDto>;

    public class CreateUserHandler : IRequestHandler<CreateUser, UserDto>
    {
        private readonly IRepository<User> _userRepository;

        public CreateUserHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserDto> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var user = new User() { Name = request.Name, Email = request.Email, Id = GetNextId() };
            _userRepository.Add(user);
            return Task.FromResult(UserDto.FromUser(user));
        }

        private int GetNextId() 
        {
            return _userRepository.GetLastId();
        }
    }

}
