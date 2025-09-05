using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Abstractions.Security;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<RefreshTokenCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenCommandHandler(
            IRefreshTokenRepository refreshTokenRepository,
            IJwtTokenGenerator jwtTokenGenerator,
            ILogger<RefreshTokenCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var storedToken = await _refreshTokenRepository.GetByTokenAsync(dto.RefreshToken);

            if (storedToken == null)
            {
                _logger.LogWarning("Invalid or non-existent refresh token: {Token}", dto.RefreshToken);
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            if (storedToken.IsRevoked || storedToken.ExpiresAt < DateTime.UtcNow)
            {
                _logger.LogWarning("Replay attack detected or token expired: {Token}", dto.RefreshToken);

                // Защита: отзываем все токены пользователя
                await _refreshTokenRepository.RevokeAllAsync(storedToken.UserId);
                throw new UnauthorizedAccessException("Detected reuse of revoked or expired refresh token.");
            }

            var user = storedToken.User;

            // Помечаем текущий токен как отозванный (удалить или revoke)
            await _refreshTokenRepository.RevokeAsync(storedToken);

            var newAccessToken = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.UserName, user.Role);
            var newRefreshToken = new RefreshToken
            {
                Token = _jwtTokenGenerator.GenerateRefreshToken(),
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            await _refreshTokenRepository.AddAsync(newRefreshToken);
            await _unitOfWork.SaveAsync();

            return new AuthResponse
            {
                AccessToken = newAccessToken,
                AccessTokenExpiration = DateTime.UtcNow.AddMinutes(15),
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.ExpiresAt,
                UserInfo = new UserAccountInfoDto
                {
                    UserName = user.UserName,
                    AvatarUrl = user.AvatarUrl
                }
            };
        }
    }
}
