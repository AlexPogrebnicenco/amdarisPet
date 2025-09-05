using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Lessons.Dto;

namespace OnlineCoursesPlatform.Application.Features.Lessons.Queries.Handlers
{
    public class GetLessonByOrderNumberHandler : IRequestHandler<GetLessonByOrderNumberQuery, LessonDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetLessonByOrderNumberHandler> _logger;
        private readonly IMapper _mapper;

        public GetLessonByOrderNumberHandler(IUnitOfWork unitOfWork, ILogger<GetLessonByOrderNumberHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<LessonDto> Handle(GetLessonByOrderNumberQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _unitOfWork.LessonRepository.GetByOrderNumberAsync(request.CourseId, request.OrderNumber);
            if (lesson == null)
            {
                _logger.LogWarning("Lesson not found for CourseId {CourseId} and OrderNumber {OrderNumber}.", request.CourseId, request.OrderNumber);
                throw new Exception("Lesson not found.");
            }
            _logger.LogInformation("Lesson with ID = {LessonId} retrieved successfully.", lesson.Id);
            return _mapper.Map<LessonDto>(lesson);
        }
    }
}
