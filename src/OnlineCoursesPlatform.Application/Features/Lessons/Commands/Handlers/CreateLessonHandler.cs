using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Lessons.Commands.Handlers
{
    public class CreateLessonHandler : IRequestHandler<CreateLessonCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateLessonHandler> _logger;
        private readonly IMapper _mapper;

        public CreateLessonHandler(IUnitOfWork unitOfWork, ILogger<CreateLessonHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                _logger.LogWarning("Course with Id {CourseId} not found.", request.CourseId);
                throw new Exception("Course not found.");
            }

            var lesson = _mapper.Map<Lesson>(request.Dto);
            lesson.CourseId = request.CourseId;

            await _unitOfWork.LessonRepository.AddAsync(lesson);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Lesson with Id {LessonId} created for Course {CourseId}.", lesson.Id, request.CourseId);

            return lesson.Id;
        }
    }
}
