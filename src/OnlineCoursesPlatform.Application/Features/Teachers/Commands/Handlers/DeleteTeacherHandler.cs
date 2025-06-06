using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;

namespace OnlineCoursesPlatform.Application.Features.Teachers.Commands.Handlers
{
    public class DeleteTeacherHandler : IRequestHandler<DeleteTeacher, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteTeacherHandler> _logger;

        public DeleteTeacherHandler(IUnitOfWork unitOfWork, ILogger<DeleteTeacherHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteTeacher request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete teacher with ID: {TeacherId}", request.Id);

            var teacher = await _unitOfWork.TeacherRepository.GetByIdAsync(request.Id);
            if (teacher == null)
            {
                _logger.LogWarning("Teacher with ID: {TeacherId} not found", request.Id);
                return false;
            }

            await _unitOfWork.TeacherRepository.RemoveAsync(teacher);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Teacher with ID: {TeacherId} was successfully deleted", teacher.Id);

            return true;
        }
    }
}
