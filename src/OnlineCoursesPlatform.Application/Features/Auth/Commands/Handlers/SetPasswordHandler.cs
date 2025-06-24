using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Abstractions.Security;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands.Handlers
{
    public class SetPasswordHandler : IRequestHandler<SetPasswordCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<SetPasswordHandler> _logger;

        public SetPasswordHandler(
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            ILogger<SetPasswordHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task<Unit> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // Проверяем совпадение паролей
            if (dto.Password != dto.ConfirmPassword)
            {
                _logger.LogWarning("Password mismatch for token: {Token}", dto.Token);
                throw new InvalidOperationException("Passwords do not match.");
            }

            // Проверяем существование токена
            var tokenEntity = await _unitOfWork.SetPasswordTokenRepository.GetByTokenAsync(dto.Token);
            if (tokenEntity == null)
            {
                _logger.LogWarning("Token not found: {Token}", dto.Token);
                throw new InvalidOperationException("Invalid or expired token.");
            }

            // Проверяем, не использован ли токен
            if (tokenEntity.IsUsed)
            {
                _logger.LogWarning("Token already used: {Token}", dto.Token);
                throw new InvalidOperationException("Token has already been used.");
            }

            // Проверяем, не истёк ли токен
            if (tokenEntity.ExpiresAt < DateTime.UtcNow)
            {
                _logger.LogWarning("Token expired: {Token}", dto.Token);
                throw new InvalidOperationException("Token has expired.");
            }

            // Проверяем, существует ли пользователь
            var user = await _unitOfWork.UserRepository.GetByIdAsync(tokenEntity.UserId);
            if (user == null)
            {
                _logger.LogError("User not found for token: {Token}", dto.Token);
                throw new InvalidOperationException("User not found.");
            }

            // Устанавливаем новый пароль
            user.Password = _passwordHasher.Hash(dto.Password);

            // Помечаем токен как использованный
            tokenEntity.IsUsed = true;

            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Password successfully set for user: {Email} with token: {Token}", user.Email, dto.Token);

            return Unit.Value;
        }
    }
}
