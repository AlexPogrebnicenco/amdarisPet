using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;

namespace OnlineCoursesPlatform.Application.Features.Courses.Commands.Handlers
{
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteCourseHandler> _logger;

        public DeleteCourseHandler(IUnitOfWork unitOfWork, ILogger<DeleteCourseHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(request.Id);
            if (course is null)
            {
                _logger.LogWarning("Course with Id: {CourseId} not found.", request.Id);
                throw new Exception("Course not found.");
            }

            await _unitOfWork.CourseRepository.RemoveAsync(course);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Course with Id: {CourseId} was successfully deleted.", request.Id);

            return Unit.Value;
        }
    }
}
