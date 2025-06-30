// src/Application/Features/Auth/Commands/Handlers/ForgotPasswordHandler.cs
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Abstractions.Services;
using OnlineCoursesPlatform.Application.Common.Exceptions;
using OnlineCoursesPlatform.Application.Settings;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands.Handlers
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ILogger<ForgotPasswordHandler> _logger;
        private readonly FrontendSettings _frontendSettings;

        public ForgotPasswordHandler(
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            ILogger<ForgotPasswordHandler> logger,
            IOptions<FrontendSettings> frontendSettings)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _logger = logger;
            _frontendSettings = frontendSettings.Value;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogWarning("User not found for email: {Email}", request.Email);
                throw new AuthException("No account found with this email.");
            }

            var token = Guid.NewGuid().ToString();

            var setPasswordToken = new SetPasswordToken
            {
                Token = token,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                IsUsed = false
            };

            await _unitOfWork.SetPasswordTokenRepository.AddAsync(setPasswordToken);
            await _unitOfWork.SaveAsync();

            var passwordResetLink = $"{_frontendSettings.BaseUrl}/set-password?token={token}";

            await _emailService.SendEmailAsync(
                user.Email,
                "Password Reset Request",
                $"<p>Hello {user.UserName},</p><p>You can reset your password using the following link:</p><p><a href='{passwordResetLink}'>{passwordResetLink}</a></p>"
            );

            _logger.LogInformation("Password reset link sent to: {Email}", user.Email);

            return Unit.Value;
        }
    }
}
