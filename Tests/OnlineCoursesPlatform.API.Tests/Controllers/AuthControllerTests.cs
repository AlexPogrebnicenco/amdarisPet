using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;
using OnlineCoursesPlatform.Domain.Entities;
using Xunit;

namespace OnlineCoursesPlatform.API.Tests.Controllers
{
    public class AuthControllerTests : IntegrationTestBase
    {
        public AuthControllerTests(CustomWebApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task GivenValidUserData_WhenRegisteringUser_ThenReturnsOk()
        {
            var registerDto = new RegisterDto
            {
                UserName = "testuser",
                Email = $"testuser_{Guid.NewGuid()}@example.com",
                Password = "Ac201220Ap*",
                ConfirmPassword = "Ac201220Ap*",
                Age = 25,
                Gender = "Male",
                Role = "User"
            };

            var response = await _client.PostAsync("/api/auth/register-user", GetPayload(registerDto));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GivenRegisteredUser_WhenLoggingIn_ThenReturnsOk()
        {
            // Регистрация
            var email = $"testuser_{Guid.NewGuid()}@example.com";
            var registerDto = new RegisterDto
            {
                UserName = "testuser",
                Email = email,
                Password = "Ac201220Ap*",
                ConfirmPassword = "Ac201220Ap*",
                Age = 25,
                Gender = "Male",
                Role = "User"
            };

            var registerResponse = await _client.PostAsync("/api/auth/register-user", GetPayload(registerDto));
            Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);

            // Логин
            var loginDto = new LoginDto
            {
                Email = email,
                Password = "Ac201220Ap*"
            };

            var loginResponse = await _client.PostAsync("/api/auth/login", GetPayload(loginDto));
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
        }

        [Fact]
        public async Task GivenValidSetPasswordToken_WhenSettingNewPassword_ThenReturnsOk()
        {
            // Регистрация
            var email = $"testuser_{Guid.NewGuid()}@example.com";
            var registerDto = new RegisterDto
            {
                UserName = "testuser",
                Email = email,
                Password = "Ac201220Ap*",
                ConfirmPassword = "Ac201220Ap*",
                Age = 25,
                Gender = "Male",
                Role = "User"
            };

            var registerResponse = await _client.PostAsync("/api/auth/register-user", GetPayload(registerDto));
            Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);

            // Достаём пользователя из базы
            using var scope = Factory.Services.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var user = await unitOfWork.UserRepository.GetByEmailAsync(email);
            Assert.NotNull(user);

            // Генерируем валидный токен
            var token = Guid.NewGuid().ToString();
            var setPasswordToken = new SetPasswordToken
            {
                UserId = user.Id,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30),
                IsUsed = false
            };

            await unitOfWork.SetPasswordTokenRepository.AddAsync(setPasswordToken);
            await unitOfWork.SaveAsync();

            // Меняем пароль
            var setPasswordDto = new SetPasswordDto
            {
                Token = token,
                Password = "NewPassword123!",
                ConfirmPassword = "NewPassword123!"
            };

            var response = await _client.PostAsync("/api/auth/set-password", GetPayload(setPasswordDto));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
