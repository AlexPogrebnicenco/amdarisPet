using OnlineCoursesPlatform.Application.Features.Users.Commands;
using OnlineCoursesPlatform.Application.Interfaces;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Repositories;
using Xunit;

namespace OnlineCoursesPlatform.Tests
{
    public class CreateUserTest
    {
        private readonly IRepository<User> _userRepository;
        private readonly CreateUserHandler _handler;

        public CreateUserTest()
        {
            _userRepository = new InMemoryRepository<User>();
            _handler = new CreateUserHandler(_userRepository);
        }

        [Fact]
        public async Task Handle_CreateUser_ShouldReturnUserDto()
        {
            var createUserCommand = new CreateUser("Alex", "alex@pogreb.com");
            var result = await _handler.Handle(createUserCommand, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(createUserCommand.Name, result.Name);
            Assert.Equal(createUserCommand.Email, result.Email);
        }

        [Fact]
        public async Task Handle_CreateUser_ShouldAddUserToRepository()
        {
            var createUserCommand = new CreateUser("Alex", "alex@pogreb.com");
            await _handler.Handle(createUserCommand,CancellationToken.None);
            var createdUser = _userRepository.GetById(1);
            Assert.NotNull(createdUser);
            Assert.Equal(createUserCommand.Name, createdUser.Name);
            Assert.Equal(createUserCommand.Email, createdUser.Email);
        }
    }
}
