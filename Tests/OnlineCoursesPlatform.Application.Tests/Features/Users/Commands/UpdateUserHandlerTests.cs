using AutoMapper;
using Azure.Core;
using Microsoft.Extensions.Logging;
using Moq;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Common.Exceptions;
using OnlineCoursesPlatform.Application.Features.Users.Commands;
using OnlineCoursesPlatform.Application.Features.Users.Commands.Handlers;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Application.Interfaces.Repositories;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Tests.Features.Users.Commands
{
    public class UpdateUserHandlerTests
    {
        private readonly int _userId = 1;
        private readonly UpdateUserDto _dto = new() { UserName = "Updated", Email = "updated@example.com" };
        private readonly UpdateUser _request;
        private readonly User _user;
        private readonly UserDto _userDto;

        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<UpdateUserHandler>> _mockLogger;

        private readonly UpdateUserHandler _handler;

        public UpdateUserHandlerTests()
        {
            _request = new UpdateUser(_userId, _dto);
            _user = new User { Id = _userId, UserName = "Old", Email = "old@example.com" };
            _userDto = new UserDto { Id = _userId, UserName = "Updated", Email = "updated@example.com" };

            _mockUserRepo = new Mock<IUserRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.UserRepository).Returns(_mockUserRepo.Object);

            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<UpdateUserHandler>>();

            _handler = new UpdateUserHandler(_mockUnitOfWork.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GivenUserExists_WhenUpdating_ThenShouldUpdateAndReturnUserDto()
        {
            _mockUserRepo.Setup(r => r.GetByIdAsync(_userId)).ReturnsAsync(_user);
            _mockUserRepo.Setup(r => r.UpdateAsync(It.Is<User>(u => u.Id == _userId))).ReturnsAsync(_user);
            _mockUnitOfWork.Setup(u => u.SaveAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map(_dto, _user));
            _mockMapper.Setup(m => m.Map<UserDto>(_user)).Returns(_userDto);

            var result = await _handler.Handle(_request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(_userDto.Email, result.Email);
            Assert.Equal(_userDto.UserName, result.UserName);

            _mockUserRepo.Verify(r => r.UpdateAsync(_user), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GivenUserExists_WhenUpdating_ThenShouldLogInformation()
        {
            _mockUserRepo.Setup(r => r.GetByIdAsync(_userId)).ReturnsAsync(_user);
            _mockUserRepo.Setup(r => r.UpdateAsync(_user)).ReturnsAsync(_user);
            _mockUnitOfWork.Setup(u => u.SaveAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map(_dto, _user));
            _mockMapper.Setup(m => m.Map<UserDto>(_user)).Returns(_userDto);

            await _handler.Handle(_request, CancellationToken.None);

            _mockLogger.VerifyLog(log => log.LogInformation("Updating user with ID: {UserId}", _userId), Times.Once());
            _mockLogger.VerifyLog(log => log.LogInformation("User with ID: {UserId} was successfully updated", _userId), Times.Once());
        }

        [Fact]
        public async Task GivenUserNotFound_WhenUpdating_ThenShouldLogWarningAndThrow()
        {
            // Arrange
            _mockUserRepo.Setup(r => r.GetByIdAsync(_userId)).ReturnsAsync((User?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(_request, CancellationToken.None));
            Assert.Equal($"User with ID {_request.Id} not found", exception.Message);

            _mockLogger.VerifyLog(log => log.LogWarning("User with ID: {UserId} not found", _userId), Times.Once());
            _mockUserRepo.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
        }
    }
}
