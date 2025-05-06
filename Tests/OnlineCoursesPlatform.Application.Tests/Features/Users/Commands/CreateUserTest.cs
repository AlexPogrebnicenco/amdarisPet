using Moq;
using OnlineCoursesPlatform.Application.Features.Users.Commands;
using OnlineCoursesPlatform.Application.Interfaces;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Tests
{
    public class CreateUserTest
    {
        private readonly Mock<IRepository<User>> _userRepositoryMock;
        private readonly CreateUserHandler _handler;

        public CreateUserTest()
        {
            _userRepositoryMock = new Mock<IRepository<User>>();
            _handler = new CreateUserHandler(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_CreateUser_ShouldReturnUserDto()
        {
            var createUserCommand = new CreateUser("Alex", "alex@pogreb.com");
            var user = new User { Id = 1, Name = createUserCommand.Name, Email = createUserCommand.Email };
            _userRepositoryMock.Setup(repo => repo.Add(It.IsAny<User>())).Callback<User>(u => u.Id = 1); // mock Add method

            var result = await _handler.Handle(createUserCommand, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(createUserCommand.Name, result.Name);
            Assert.Equal(createUserCommand.Email, result.Email);
        }

        [Fact]
        public async Task Handle_CreateUser_ShouldCallAddMethodOnce()
        {
            var createUserCommand = new CreateUser("Alex", "alex@pogreb.com");

            await _handler.Handle(createUserCommand, CancellationToken.None);

            _userRepositoryMock.Verify(repo => repo.Add(It.IsAny<User>()), Times.Once);
        }
    }
}
