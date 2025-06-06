using Microsoft.Extensions.Logging;
using Moq;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Common.Exceptions;
using OnlineCoursesPlatform.Application.Features.Users.Commands;
using OnlineCoursesPlatform.Application.Features.Users.Commands.Handlers;
using OnlineCoursesPlatform.Application.Interfaces.Repositories;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Tests.Features.Users.Commands
{
    public class DeleteUserHandlerTests
    {
        private readonly int _userId = 1;
        private readonly User _user;
        private readonly DeleteUser _request;
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogger<DeleteUserHandler>> _mockLogger;
        private readonly DeleteUserHandler _handler;

        public DeleteUserHandlerTests()
        {
            _user = new User { Id = _userId, UserName = "Test", Email = "test@example.com" };
            _request = new DeleteUser(_userId);

            _mockUserRepo = new Mock<IUserRepository>();
            _mockUserRepo.Setup(r => r.GetByIdAsync(_userId)).ReturnsAsync(_user);
            _mockUserRepo.Setup(r => r.RemoveAsync(_user)).Returns(Task.CompletedTask);

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.UserRepository).Returns(_mockUserRepo.Object);
            _mockUnitOfWork.Setup(u => u.SaveAsync()).Returns(Task.CompletedTask);

            _mockLogger = new Mock<ILogger<DeleteUserHandler>>();

            _handler = new DeleteUserHandler(_mockUnitOfWork.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GivenExistingUser_WhenHandleIsCalled_ThenRemovesUserAndReturnsTrue()
        {
            var result = await _handler.Handle(_request, CancellationToken.None);

            Assert.True(result);
            _mockUserRepo.Verify(r => r.RemoveAsync(_user), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GivenNonExistingUser_WhenHandleIsCalled_ThenThrowsNotFoundException()
        {
            // Arrange
            var userId = 99;
            var request = new DeleteUser(userId);
            _mockUserRepo.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User)null!);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal($"User with ID {userId} not found", exception.Message);
            _mockUserRepo.Verify(r => r.RemoveAsync(It.IsAny<User>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task GivenExistingUser_WhenHandleIsCalled_ThenLogsAllSteps()
        {
            await _handler.Handle(_request, CancellationToken.None);

            _mockLogger.VerifyLog(log => log.LogInformation("Attempting to delete user with ID: {UserId}", _userId), Times.Once());
            _mockLogger.VerifyLog(log => log.LogInformation("User with ID: {UserId} was successfully deleted", _userId), Times.Once());
        }
    }
}
