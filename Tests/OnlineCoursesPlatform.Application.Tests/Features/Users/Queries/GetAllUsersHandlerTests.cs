using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Application.Features.Users.Queries;
using OnlineCoursesPlatform.Application.Features.Users.Queries.Handlers;
using OnlineCoursesPlatform.Application.Interfaces.Repositories;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Tests.Features.Users.Queries
{
    public class GetAllUsersHandlerTests
    {
        private readonly int _pageNumber = 1;
        private readonly int _pageSize = 10;

        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<GetAllUsersHandler>> _mockLogger;

        private readonly GetAllUsersHandler _handler;

        public GetAllUsersHandlerTests()
        {
            _mockUserRepo = new Mock<IUserRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.UserRepository).Returns(_mockUserRepo.Object);

            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<GetAllUsersHandler>>();

            _handler = new GetAllUsersHandler(_mockUnitOfWork.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GivenUsersExist_WhenHandleCalled_ThenReturnsMappedUserDtos()
        {
            // Arrange 
            var users = new List<User>
            {
                new User { Id = 1, UserName = "User1", Email = "user1@example.com" },
                new User { Id = 2, UserName = "User2", Email = "user2@example.com" }
            };

            var userDtos = new List<UserDto>
            {
                new UserDto { Id = 1, UserName = "User1", Email = "user1@example.com" },
                new UserDto { Id = 2, UserName = "User2", Email = "user2@example.com" }
            };

            _mockUserRepo.Setup(r => r.GetAllAsync(_pageNumber, _pageSize)).ReturnsAsync(users);
            _mockMapper.Setup(m => m.Map<IEnumerable<UserDto>>(users)).Returns(userDtos);

            var request = new GetAllUsers(_pageNumber, _pageSize);

            // Act 
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("User1", result.First().UserName);
        }

        [Fact]
        public async Task GivenNoUsersExist_WhenHandleCalled_ThenReturnsEmptyList()
        {
            // Arrange 
            var users = new List<User>();
            var userDtos = new List<UserDto>();

            _mockUserRepo.Setup(r => r.GetAllAsync(_pageNumber, _pageSize)).ReturnsAsync(users);
            _mockMapper.Setup(m => m.Map<IEnumerable<UserDto>>(users)).Returns(userDtos);

            var request = new GetAllUsers(_pageNumber, _pageSize);

            // Act 
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert 
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task WhenHandleCalled_ThenLogsRequesrAndResult()
        {
            // Arrange 
            var users = new List<User> { new User { Id = 1, UserName = "User1", Email = "user1@example.com" } };
            var userDtos = new List<UserDto> { new UserDto { UserName = "User1", Email = "user1@example.com" } };

            _mockUserRepo.Setup(r => r.GetAllAsync(_pageNumber, _pageSize)).ReturnsAsync(users);
            _mockMapper.Setup(m => m.Map<IEnumerable<UserDto>>(users)).Returns(userDtos);

            var request = new GetAllUsers(_pageNumber, _pageSize);

            // Act 
            await _handler.Handle(request, CancellationToken.None);

            // Assert 
            _mockLogger.VerifyLog(log => log.LogInformation("Requested list of users (page: {PageNumber}, size: {PageSize})", _pageNumber, _pageSize), Times.Once);
            _mockLogger.VerifyLog(log => log.LogInformation("Returned {Count} users from page {PageNumber}", 1, _pageNumber), Times.Once);
        }
    }
}
