using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;
using OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Commands;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Commands.Handlers
{
    public class CreateTeacherRegistrationRequestHandler : IRequestHandler<CreateTeacherRegistrationRequestCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateTeacherRegistrationRequestHandler> _logger;

        public CreateTeacherRegistrationRequestHandler(IUnitOfWork unitOfWork, ILogger<CreateTeacherRegistrationRequestHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateTeacherRegistrationRequestCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Teacher registration failed: Email already exists - {Email}", dto.Email);
                throw new InvalidOperationException("User with this email already exists.");
            }

            var teacherRequest = new TeacherRegistrationRequest
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Age = dto.Age,
                Gender = dto.Gender
            };

            await _unitOfWork.TeacherRegistrationRequestRepository.AddAsync(teacherRequest);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Teacher registration request created: {Email}", dto.Email);

            return Unit.Value;
        }
    }
}
