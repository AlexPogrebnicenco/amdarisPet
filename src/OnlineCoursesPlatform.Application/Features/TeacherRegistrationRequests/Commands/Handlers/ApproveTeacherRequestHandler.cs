using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Commands.Handlers
{
    public class ApproveTeacherRequestHandler : IRequestHandler<ApproveTeacherRequestCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ApproveTeacherRequestHandler> _logger;
        private readonly IMapper _mapper;

        public ApproveTeacherRequestHandler(IUnitOfWork unitOfWork, ILogger<ApproveTeacherRequestHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(ApproveTeacherRequestCommand request, CancellationToken cancellationToken)
        {
            var teacherRequest = await _unitOfWork.TeacherRegistrationRequestRepository.GetByIdAsync(request.RequestId);

            if (teacherRequest == null)
            {
                _logger.LogWarning("Teacher request not found: {RequestId}", request.RequestId);
                throw new InvalidOperationException("Request not found.");
            }

            var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(teacherRequest.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("User with email already exists: {Email}", teacherRequest.Email);
                throw new InvalidOperationException("User with this email already exists.");
            }

            teacherRequest.Status = "Approved";

            var newUser = _mapper.Map<User>(teacherRequest);

            await _unitOfWork.UserRepository.AddAsync(newUser);
            await _unitOfWork.SaveAsync(); // Сохраняем, чтобы получить Id пользователя

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

            var passwordSetupLink = $"https://your-frontend.com/set-password?token={token}";

            _logger.LogInformation("Set password link: {Link}", passwordSetupLink);
            _logger.LogInformation("Teacher request approved: {RequestId} | User created: {Email}", request.RequestId, newUser.Email);

            return Unit.Value;
        }
    }
}
