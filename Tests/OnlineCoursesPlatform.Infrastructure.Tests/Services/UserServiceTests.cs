using Moq;
using OnlineCoursesPlatform.Application.Interfaces;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Repositories;
using Xunit;

namespace OnlineCoursesPlatform.Infrastructure.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IRepository<User>> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IRepository<User>>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public void AddUserShouldCallAddMethod()
        {
            var user = new User { Id = 1, UserName = "Alex", Email = "alex@pogreb.com" };
            _userService.Add(user);
            _userRepositoryMock.Verify(repo => repo.Add(user), Times.Once);
        }
        [Fact]
        public void GetById_UserExists_ShouldReturnUser()
        {
            var userId = 1;
            var user = new User { Id = userId, UserName = "Alex", Email = "alex@pogreb.com" };
            _userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns(user);

            var result = _userService.GetById(userId);

            Assert.Equal(user, result);
        }

        [Fact]
        public void GetById_UserDoesNotExist_ShouldThrowException()
        {
            var userId = 1;
            _userRepositoryMock.Setup(repo => repo.GetById(userId)).Throws(new InvalidOperationException("User not found"));

            Assert.Throws<InvalidOperationException>(() => _userService.GetById(userId));
        }

        [Fact]
        public void Update_User_ShouldCallUpdateMethod()
        {
            var user = new User { Id = 1, UserName = "Alex", Email = "alex@pogreb.com" };

            _userService.Update(user);

            _userRepositoryMock.Verify(repo => repo.Update(user), Times.Once);
        }

        [Fact]
        public void Delete_User_ShouldCallDeleteMethod()
        {
            var userId = 1;

            _userService.Delete(userId);

            _userRepositoryMock.Verify(repo => repo.Delete(userId), Times.Once);
        }
    }
}