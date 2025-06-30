using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;

namespace OnlineCoursesPlatform.Application.Features.Courses.Commands.Handlers
{
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateCourseHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateCourseHandler(IUnitOfWork unitOfWork, ILogger<UpdateCourseHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(request.Id);
            if (course is null)
            {
                _logger.LogWarning("Course with Id: {CourseId} not found.", request.Id);
                throw new Exception("Course not found.");
            }

            _mapper.Map(request.Dto, course);

            // Меняем дату обновления вручную
            course.DateModified = DateTime.UtcNow;

            await _unitOfWork.CourseRepository.UpdateAsync(course);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Course with Id: {CourseId} was successfully updated.", request.Id);

            return Unit.Value;
        }
    }
}
