using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Abstractions.Security;
using OnlineCoursesPlatform.Application.Common.Exceptions;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUser, AuthResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<LoginUserHandler> _logger;

        public LoginUserHandler(
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator,
            ILogger<LoginUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
        }

        public async Task<AuthResponse> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var user = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                _logger.LogWarning("Login failed: user not found with email {Email}", dto.Email);
                throw new UnauthenticatedException("Invalid credentials");
            }

            if (!string.IsNullOrEmpty(user.ExternalProvider))
            {
                _logger.LogWarning("Login failed: user with email {Email} registered via {Provider}", dto.Email, user.ExternalProvider);
                throw new UnauthenticatedException($"This account is linked to {user.ExternalProvider}. Use {user.ExternalProvider} login.");
            }

            if (user.Password == null)
            {
                _logger.LogError("Login failed: user {Email} has no password set, but is not an external provider.", dto.Email);
                throw new UnauthenticatedException("Invalid credentials");
            }

            if (!_passwordHasher.Verify(dto.Password, user.Password))
            {
                _logger.LogWarning("Login failed: invalid password for email {Email}", dto.Email);
                throw new UnauthenticatedException("Invalid credentials");
            }

            // Генерация токенов
            var accessToken = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.UserName, user.Role);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshTokenEntity);
            await _unitOfWork.SaveAsync();

            return new AuthResponse
            {
                AccessToken = accessToken,
                AccessTokenExpiration = DateTime.UtcNow.AddMinutes(15), 
                RefreshToken = refreshToken,
                RefreshTokenExpiration = refreshTokenEntity.ExpiresAt,
                UserInfo = new UserAccountInfoDto
                {
                    UserName = user.UserName,
                    AvatarUrl = user.AvatarUrl
                }
            };
        }
    }
}
