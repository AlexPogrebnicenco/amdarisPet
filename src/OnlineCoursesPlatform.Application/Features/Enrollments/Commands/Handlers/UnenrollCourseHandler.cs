using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Enrollments.Commands;

namespace OnlineCoursesPlatform.Application.Features.Enrollments.Commands.Handlers
{
    public class UnenrollCourseHandler : IRequestHandler<UnenrollCourseCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UnenrollCourseHandler> _logger;

        public UnenrollCourseHandler(IUnitOfWork unitOfWork, ILogger<UnenrollCourseHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(UnenrollCourseCommand request, CancellationToken cancellationToken)
        {
            // Ищем подписку по UserId и CourseId
            var enrollment = await _unitOfWork.EnrollmentRepository
                .GetByUserIdAndCourseIdAsync(request.UserId, request.CourseId);

            if (enrollment == null)
            {
                _logger.LogWarning("Enrollment not found for UserId: {UserId} and CourseId: {CourseId}", request.UserId, request.CourseId);
                throw new Exception("Enrollment not found.");
            }

            // Удаляем подписку
            await _unitOfWork.EnrollmentRepository.RemoveAsync(enrollment);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("UserId: {UserId} successfully unenrolled from CourseId: {CourseId}", request.UserId, request.CourseId);

            return Unit.Value;
        }
    }
}
