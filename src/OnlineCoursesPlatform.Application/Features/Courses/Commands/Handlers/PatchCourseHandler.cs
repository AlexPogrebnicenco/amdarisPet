using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;

namespace OnlineCoursesPlatform.Application.Features.Courses.Commands.Handlers
{
    public class PatchCourseHandler : IRequestHandler<PatchCourseCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PatchCourseHandler> _logger;
        private readonly IMapper _mapper;

        public PatchCourseHandler(IUnitOfWork unitOfWork, ILogger<PatchCourseHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(PatchCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(request.Id);
            if (course is null)
            {
                _logger.LogWarning("Course with Id: {CourseId} not found.", request.Id);
                throw new Exception("Course not found.");
            }

            // Маппим сущность в DTO
            var courseDto = new UpdateCourseDto
            {
                Title = course.Title,
                Description = course.Description,
                CategoryId = course.CategoryId,
                Difficulty = course.Difficulty,
                About = course.About
            };

            // Применяем патч
            request.PatchDoc.ApplyTo(courseDto);

            // Маппим DTO обратно в сущность
            _mapper.Map(courseDto, course);
            course.DateModified = DateTime.UtcNow;

            await _unitOfWork.CourseRepository.UpdateAsync(course);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Course with Id: {CourseId} was partially updated.", request.Id);

            return Unit.Value;
        }
    }
}
