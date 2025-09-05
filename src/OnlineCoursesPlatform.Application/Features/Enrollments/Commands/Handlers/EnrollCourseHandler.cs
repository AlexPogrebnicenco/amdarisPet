using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Abstractions.Services;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Enrollments.Commands.Handlers
{
    public class EnrollCourseHandler : IRequestHandler<EnrollCourseCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EnrollCourseHandler> _logger;
        private readonly IRealTimeNotifier _realTimeNotifier;

        public EnrollCourseHandler(
            IUnitOfWork unitOfWork,
            ILogger<EnrollCourseHandler> logger,
            IRealTimeNotifier realTimeNotifier)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _realTimeNotifier = realTimeNotifier;
        }

        public async Task<Unit> Handle(EnrollCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                _logger.LogWarning("Course with Id: {CourseId} not found.", request.CourseId);
                throw new Exception("Course not found.");
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                _logger.LogWarning("User with Id: {UserId} not found.", request.UserId);
                throw new Exception("User not found.");
            }

            // Проверка на повторную подписку
            var existingEnrollment = await _unitOfWork.EnrollmentRepository
                .GetByUserIdAndCourseIdAsync(request.UserId, request.CourseId);

            if (existingEnrollment != null)
            {
                _logger.LogWarning("UserId: {UserId} is already enrolled in CourseId: {CourseId}", request.UserId, request.CourseId);
                throw new Exception("User is already enrolled in this course.");
            }

            var enrollment = new Enrollment
            {
                UserId = request.UserId,
                CourseId = request.CourseId,
                EnrolledAt = DateTime.UtcNow
            };

            await _unitOfWork.EnrollmentRepository.AddAsync(enrollment);
            await _unitOfWork.SaveAsync();

            // Отправляем уведомление всем авторам курса
            var authors = course.CourseAuthors;

            if (authors == null || !authors.Any())
            {
                _logger.LogWarning("No authors found for CourseId: {CourseId}", request.CourseId);
                throw new Exception("No authors found for this course.");
            }

            foreach (var author in authors)
            {
                await _realTimeNotifier.SendEnrollmentNotification(
                    author.UserId,
                    course.Title,
                    user.Email,
                    enrollment.EnrolledAt
                );
            }

            _logger.LogInformation("UserId: {UserId} successfully enrolled in CourseId: {CourseId}", request.UserId, request.CourseId);

            return Unit.Value;
        }
    }
}
