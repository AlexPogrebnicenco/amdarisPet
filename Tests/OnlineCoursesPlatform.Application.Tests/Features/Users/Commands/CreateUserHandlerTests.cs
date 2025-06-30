//using AutoMapper;
//using Azure.Core;
//using Microsoft.Extensions.Logging;
//using Moq;
//using OnlineCoursesPlatform.Application.Abstractions.Repositories;
//using OnlineCoursesPlatform.Application.Common.Exceptions;
//using OnlineCoursesPlatform.Application.Features.Users.Commands;
//using OnlineCoursesPlatform.Application.Features.Users.Commands.Handlers;
//using OnlineCoursesPlatform.Application.Features.Users.Dto;
//using OnlineCoursesPlatform.Application.Interfaces.Repositories;
//using OnlineCoursesPlatform.Domain.Entities;


//namespace OnlineCoursesPlatform.Application.Tests.Features.Users.Commands
//{
//    public class CreateUserHandlerTests
//    {
//        private readonly CreateUserDto _requestDto;
//        private readonly CreateUser _request;
//        private readonly User _user;
//        private readonly UserDto _userDto;

//        private readonly Mock<IUserRepository> _mockUserRepo;
//        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
//        private readonly Mock<IMapper> _mockMapper;
//        private readonly Mock<ILogger<CreateUserHandler>> _mockLogger;

//        private readonly CreateUserHandler _handler;

//        public CreateUserHandlerTests()
//        {
//            _requestDto = new CreateUserDto { UserName = "Test", Email = "test@example.com" };
//            _request = new CreateUser(_requestDto);
//            _user = new User { Id = 1, UserName = "Test", Email = "test@example.com", Password = "stub" };
//            _userDto = new UserDto { Id = 1, UserName = "Test", Email = "test@example.com" };

//            _mockUserRepo = new Mock<IUserRepository>();
//            _mockUserRepo.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(_user);

//            _mockUnitOfWork = new Mock<IUnitOfWork>();
//            _mockUnitOfWork.Setup(u => u.UserRepository).Returns(_mockUserRepo.Object);
//            _mockUnitOfWork.Setup(u => u.SaveAsync()).Returns(Task.CompletedTask);

//            _mockMapper = new Mock<IMapper>();
//            _mockMapper.Setup(m => m.Map<User>(_requestDto)).Returns(_user);
//            _mockMapper.Setup(m => m.Map<UserDto>(_user)).Returns(_userDto);

//            _mockLogger = new Mock<ILogger<CreateUserHandler>>();

//            _handler = new CreateUserHandler(_mockUnitOfWork.Object, _mockMapper.Object, _mockLogger.Object);
//        }

//        [Fact]
//        public async Task GivenValidRequest_WhenHandlingCreateUser_ThenUserIsAddedAndUserDtoReturned()
//        {
//            var result = await _handler.Handle(_request, CancellationToken.None);

//            Assert.NotNull(result);
//            Assert.Equal(_userDto.UserName, result.UserName);
//            Assert.Equal(_userDto.Email, result.Email);

//            _mockUserRepo.Verify(x => x.AddAsync(_user), Times.Once);
//            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
//        }

//        [Fact]
//        public async Task GivenValidRequest_WhenHandlingCreateUser_ThenLogsAreWritten()
//        {
//            await _handler.Handle(_request, CancellationToken.None);

//            _mockLogger.VerifyLog(log => log.LogInformation("Creating user with email: {Email}", _requestDto.Email), Times.Once());
//            _mockLogger.VerifyLog(log => log.LogInformation("User created with ID: {UserId}", _user.Id), Times.Once());
//        }

//        [Fact]
//        public async Task GivedValidRequest_WhenHandlingCreateUser_ThenCorrectUserIsPassedtoAddAsync()
//        {
//            await _handler.Handle(_request, CancellationToken.None);

//            _mockUserRepo.Verify(r =>
//                r.AddAsync(It.Is<User>(u =>
//                    u.Email == _requestDto.Email &&
//                    u.UserName == _requestDto.UserName
//                )),
//                Times.Once
//            );
//        }

//        [Fact]
//        public async Task GivenEmailAlreadyExists_WhenHandlingCreateUser_ThenThrowsConflictException()
//        {
//            // Arrange
//            _mockUserRepo.Setup(r => r.GetByEmailAsync(_requestDto.Email)).ReturnsAsync(_user);

//            //Act & Assert
//            var exception = await Assert.ThrowsAsync<ConflictException>(() =>
//                _handler.Handle(_request, CancellationToken.None));

//            Assert.Equal($"User with email '{_request.Dto.Email}' already exists.", exception.Message);

//            _mockUserRepo.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
//            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
//        }
//    }
//}
