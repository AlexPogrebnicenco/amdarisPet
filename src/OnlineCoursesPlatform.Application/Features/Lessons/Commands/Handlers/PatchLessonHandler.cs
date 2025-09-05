using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Lessons.Dto;

namespace OnlineCoursesPlatform.Application.Features.Lessons.Commands.Handlers
{
    public class PatchLessonHandler : IRequestHandler<PatchLessonCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PatchLessonHandler> _logger;
        private readonly IMapper _mapper;

        public PatchLessonHandler(IUnitOfWork unitOfWork, ILogger<PatchLessonHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(PatchLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _unitOfWork.LessonRepository.GetByOrderNumberAsync(request.CourseId, request.OrderNumber);
            if (lesson == null)
            {
                _logger.LogWarning("Lesson not found for CourseId {CourseId} and OrderNumber {OrderNumber}.", request.CourseId, request.OrderNumber);
                throw new Exception("Lesson not found.");
            }

            var updateDto = _mapper.Map<UpdateLessonDto>(lesson);
            request.PatchDoc.ApplyTo(updateDto);

            _mapper.Map(updateDto, lesson);
            await _unitOfWork.LessonRepository.UpdateAsync(lesson);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Lesson with Id {LessonId} was patched.", lesson.Id);

            return Unit.Value;
        }
    }
}
