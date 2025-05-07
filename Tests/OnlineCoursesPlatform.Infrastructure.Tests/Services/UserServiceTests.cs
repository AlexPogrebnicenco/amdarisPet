using OnlineCoursesPlatform.Application.Interfaces;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Repositories;
using Xunit;

namespace OnlineCoursesPlatform.Infrastructure.Tests.Services
{
    public class UserServiceTests
    {
        private readonly IRepository<User> _userRepository;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepository = new InMemoryRepository<User>();
            _userService = new UserService(_userRepository);
        }

        [Fact]
        public void AddUserShouldCallAddMethod()
        {
            var user = new User { Id = 1, Name = "Alex", Email = "alex@pogreb.com" };
            _userService.Add(user);

            var addedUser = _userRepository.GetById(1);

            Assert.NotNull(addedUser);
            Assert.Equal(user.Name, addedUser.Name);
            Assert.Equal(user.Email, addedUser.Email);
        }

        [Fact]
        public void GetById_UserDoesNotExist_ShouldThrowException()
        {
            var userId = 1;

            Assert.Throws<InvalidOperationException>(() => _userService.GetById(userId));
        }

        [Fact]
        public void Update_User_ShouldUpdateUser()
        {
            var user = new User { Id = 1, Name = "Alex", Email = "alex@pogreb.com" };
            _userService.Add(user);

            var updatedUser = new User { Id = 1, Name = "Alex Updated", Email = "alex_updated@pogreb.com" };
            _userService.Update(updatedUser);

            var result = _userService.GetById(1);
            Assert.Equal(updatedUser.Name, result.Name);
            Assert.Equal(updatedUser.Email, result.Email);
        }

        [Fact]
        public void Delete_User_ShouldDeleteUser()
        {
            var user = new User { Id = 1, Name = "Alex", Email = "alex@pogreb.com" };
            _userService.Add(user);
            _userService.Delete(1);
            Assert.Throws<InvalidOperationException>(() => _userService.GetById(1));
        }
    }
}
