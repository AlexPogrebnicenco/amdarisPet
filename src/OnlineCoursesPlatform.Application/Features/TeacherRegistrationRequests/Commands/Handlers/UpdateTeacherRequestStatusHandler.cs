using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Abstractions.Services;
using OnlineCoursesPlatform.Application.Settings;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Enums;

namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Commands.Handlers
{
    public class UpdateTeacherRequestStatusHandler : IRequestHandler<UpdateTeacherRequestStatusCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateTeacherRequestStatusHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly FrontendSettings _frontendSettings;

        public UpdateTeacherRequestStatusHandler(
            IUnitOfWork unitOfWork,
            ILogger<UpdateTeacherRequestStatusHandler> logger,
            IMapper mapper,
            IEmailService emailService,
            IOptions<FrontendSettings> frontendSettings)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _emailService = emailService;
            _frontendSettings = frontendSettings.Value;
        }

        public async Task<Unit> Handle(UpdateTeacherRequestStatusCommand request, CancellationToken cancellationToken)
        {
            var teacherRequest = await _unitOfWork.TeacherRegistrationRequestRepository.GetByIdAsync(request.RequestId);

            if (teacherRequest == null)
                throw new InvalidOperationException("Request not found.");

            if (teacherRequest.Status != TeacherRequestStatus.Pending)
                throw new InvalidOperationException("Request has already been processed.");

            switch (request.Status)
            {
                case TeacherRequestStatus.Approved:
                    {
                        var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(teacherRequest.Email);
                        if (existingUser != null)
                            throw new InvalidOperationException("User with this email already exists.");

                        teacherRequest.Status = TeacherRequestStatus.Approved;

                        var newUser = _mapper.Map<User>(teacherRequest);

                        await _unitOfWork.UserRepository.AddAsync(newUser);
                        await _unitOfWork.SaveAsync();

                        var token = Guid.NewGuid().ToString();

                        var setPasswordToken = new SetPasswordToken
                        {
                            Token = token,
                            ExpiresAt = DateTime.UtcNow.AddDays(1),
                            IsUsed = false,
                            UserId = newUser.Id
                        };

                        await _unitOfWork.SetPasswordTokenRepository.AddAsync(setPasswordToken);
                        await _unitOfWork.SaveAsync();

                        var passwordSetupLink = $"{_frontendSettings.BaseUrl}/set-password?token={token}";

                        await _emailService.SendEmailAsync(
                            newUser.Email,
                            "Set your password",
                            $"<p>Hello {newUser.UserName},</p><p>Please set your password using the following link:</p><p><a href='{passwordSetupLink}'>{passwordSetupLink}</a></p>"
                        );

                        _logger.LogInformation("Teacher request approved: {RequestId} | User created: {Email}", request.RequestId, newUser.Email);
                        _logger.LogInformation("Set password link sent to: {Email}", newUser.Email);
                        break;
                    }

                case TeacherRequestStatus.Rejected:
                    {
                        teacherRequest.Status = TeacherRequestStatus.Rejected;
                        await _unitOfWork.SaveAsync();

                        _logger.LogInformation("Rejected teacher request: {RequestId}", request.RequestId);
                        break;
                    }

                default:
                    throw new InvalidOperationException("Invalid status provided.");
            }

            return Unit.Value;
        }
    }
}
