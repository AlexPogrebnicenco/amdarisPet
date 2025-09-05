using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;

namespace OnlineCoursesPlatform.Application.Features.Courses.Queries.Handlers
{
    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, CourseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCourseByIdHandler> _logger;

        public GetCourseByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetCourseByIdHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CourseDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseDetailsByIdAsync(request.Id);
            if (course is null)
            {
                _logger.LogWarning("Course with Id: {CourseId} not found.", request.Id);
                throw new Exception("Course not found.");
            }

            return _mapper.Map<CourseDto>(course);
        }
    }
}
