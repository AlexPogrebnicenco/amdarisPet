using OnlineCoursesPlatform.Application.Features.Users.Commands;
using OnlineCoursesPlatform.Application.Interfaces;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Services;
using OnlineCoursesPlatform.Infrastructure.Repositories;
using Xunit;

namespace OnlineCoursesPlatform.Tests
{
    public class CreateUserTest
    {
        private readonly IUserService _userService;
        private readonly CreateUserHandler _handler;

        public CreateUserTest()
        {
            var repository = new InMemoryRepository<User>();
            _userService = new UserService(repository);
            _handler = new CreateUserHandler(_userService);
        }

        [Fact]
        public async Task Handle_CreateUser_ShouldReturnUserDto()
        {
            var createUserCommand = new CreateUser("Alex", "alex@pogreb.com");
            var result = await _handler.Handle(createUserCommand, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(createUserCommand.Name, result.UserName);
            Assert.Equal(createUserCommand.Email, result.Email);
        }

        [Fact]
        public async Task Handle_CreateUser_ShouldAddUserToRepository()
        {
            var createUserCommand = new CreateUser("Alex", "alex@pogreb.com");
            await _handler.Handle(createUserCommand,CancellationToken.None);
            var createdUser = _userService.GetById(1);
            Assert.NotNull(createdUser);
            Assert.Equal(createUserCommand.Name, createdUser.UserName);
            Assert.Equal(createUserCommand.Email, createdUser.Email);
        }
    }
}
