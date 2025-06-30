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
    public class RequestNewLinkHandler : IRequestHandler<RequestNewLinkCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ILogger<RequestNewLinkHandler> _logger;
        private readonly FrontendSettings _frontendSettings;

        public RequestNewLinkHandler(
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            ILogger<RequestNewLinkHandler> logger,
            IOptions<FrontendSettings> frontendSettings)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _logger = logger;
            _frontendSettings = frontendSettings.Value;
        }

        public async Task<Unit> Handle(RequestNewLinkCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogWarning("User not found for email: {Email}", request.Email);
                throw new AuthException("No account found with this email.");
            }

            if (user.Password != null)
            {
                _logger.LogWarning("User already has a password: {Email}", request.Email);
                throw new AuthException("Password is already set for this account.");
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

            var passwordSetupLink = $"{_frontendSettings.BaseUrl}/set-password?token={token}";

            await _emailService.SendEmailAsync(
                user.Email,
                "Set your password",
                $"<p>Hello {user.UserName},</p><p>Please set your password using the following link:</p><p><a href='{passwordSetupLink}'>{passwordSetupLink}</a></p>"
            );

            _logger.LogInformation("New password link sent to: {Email}", user.Email);

            return Unit.Value;
        }
    }
}
