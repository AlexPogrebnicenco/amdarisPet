using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Courses.Commands.Handlers
{
    public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateCourseHandler> _logger;
        private readonly IMapper _mapper;

        public CreateCourseHandler(IUnitOfWork unitOfWork, ILogger<CreateCourseHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = _mapper.Map<Course>(request.Dto);

            await _unitOfWork.CourseRepository.AddAsync(course);
            await _unitOfWork.SaveAsync();

            // Связываем теги с курсом
            foreach (var tagId in request.Dto.TagIds)
            {
                var courseTag = new CourseTag
                {
                    CourseId = course.Id,
                    TagId = tagId
                };

                await _unitOfWork.CourseTagRepository.AddAsync(courseTag);
            }
            await _unitOfWork.SaveAsync();

            var courseAuthor = new CourseAuthor
            {
                CourseId = course.Id,
                UserId = request.UserId
            };

            await _unitOfWork.CourseAuthorRepository.AddAsync(courseAuthor);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Course created: {Title} by UserId: {UserId}", course.Title, request.UserId);

            return course.Id;
        }
    }
}
