using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Lessons.Dto;

namespace OnlineCoursesPlatform.Application.Features.Lessons.Queries.Handlers
{
    public class GetLessonsByCourseIdHandler : IRequestHandler<GetLessonsByCourseIdQuery, List<LessonListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetLessonsByCourseIdHandler> _logger;

        public GetLessonsByCourseIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetLessonsByCourseIdHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<LessonListDto>> Handle(GetLessonsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var lessons = await _unitOfWork.LessonRepository.GetLessonsByCourseIdAsync(request.CourseId);
            return _mapper.Map<List<LessonListDto>>(lessons);
        }
    }
}