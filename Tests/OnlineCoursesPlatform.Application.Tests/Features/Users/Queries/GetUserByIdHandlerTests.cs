using AutoMapper;
using Azure.Core;
using Microsoft.Extensions.Logging;
using Moq;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Common.Exceptions;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Application.Features.Users.Queries;
using OnlineCoursesPlatform.Application.Features.Users.Queries.Handlers;
using OnlineCoursesPlatform.Application.Interfaces.Repositories;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Tests.Features.Users.Queries
{
    public class GetUserByIdHandlerTests
    {
        private readonly int _userId = 1;
        private readonly User _user;
        private readonly UserDto _userDto;
        private readonly GetUserById _request;

        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<GetUserByIdHandler>> _mockLogger;

        private readonly GetUserByIdHandler _handler;

        public GetUserByIdHandlerTests()
        {
            _user = new User { Id = _userId, UserName = "Test", Email = "test@example.com" };
            _userDto = new UserDto { Id = _userId, UserName = "Test", Email = "test@example.com" };
            _request = new GetUserById(_userId);

            _mockUserRepo = new Mock<IUserRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.UserRepository).Returns(_mockUserRepo.Object);

            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<GetUserByIdHandler>>();

            _handler = new GetUserByIdHandler(_mockUnitOfWork.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GivenExistingUser_WhenHandleCalled_ThenReturnsUserDto()
        {
            // Arrange
            _mockUserRepo.Setup(r => r.GetByIdAsync(_userId)).ReturnsAsync(_user);
            _mockMapper.Setup(m => m.Map<UserDto>(_user)).Returns(_userDto);

            // Act 
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_userDto.Id, result.Id);
            Assert.Equal(_userDto.UserName, result.UserName);
            Assert.Equal(_userDto.Email, result.Email);
        }

        [Fact]
        public async Task GivenUserNotFound_WhenHandleCalled_ThenShouldLogWarningAndThrow()
        {
            // Arrange
            _mockUserRepo.Setup(r => r.GetByIdAsync(_userId)).ReturnsAsync((User?)null);

            // Act & Assert 
            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(_request, CancellationToken.None));
            Assert.Equal($"User with ID {_request.Id} not found", exception.Message);

            _mockLogger.VerifyLog(log => log.LogWarning("User with ID: {UserId} not found", _request.Id), Times.Once());
            _mockUserRepo.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
        }
    }
}
