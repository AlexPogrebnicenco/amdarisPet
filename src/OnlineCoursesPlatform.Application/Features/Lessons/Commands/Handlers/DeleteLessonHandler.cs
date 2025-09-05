using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;

namespace OnlineCoursesPlatform.Application.Features.Lessons.Commands.Handlers
{
    public class DeleteLessonHandler : IRequestHandler<DeleteLessonCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteLessonHandler> _logger;

        public DeleteLessonHandler(IUnitOfWork unitOfWork, ILogger<DeleteLessonHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _unitOfWork.LessonRepository.GetByOrderNumberAsync(request.CourseId, request.OrderNumber);
            if (lesson == null)
            {
                _logger.LogWarning("Lesson not found for CourseId {CourseId} and OrderNumber {OrderNumber}.", request.CourseId, request.OrderNumber);
                throw new Exception("Lesson not found.");
            }

            await _unitOfWork.LessonRepository.RemoveAsync(lesson); 
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Lesson with Id {LessonId} deleted.", lesson.Id);

            return Unit.Value;
        }
    }
}
