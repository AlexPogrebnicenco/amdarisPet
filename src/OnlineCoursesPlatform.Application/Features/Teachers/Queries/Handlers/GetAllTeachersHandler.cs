using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Teachers.Dto;

namespace OnlineCoursesPlatform.Application.Features.Teachers.Queries.Handlers
{
    public class GetAllTeachersHandler : IRequestHandler<GetAllTeachers, IEnumerable<TeacherDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllTeachersHandler> _logger;

        public GetAllTeachersHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GetAllTeachersHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<TeacherDto>> Handle(GetAllTeachers request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Requested list of teachers (page: {PageNumber}, size: {PageSize})", request.pageNumber, request.pageSize);

            var teachers = await _unitOfWork.TeacherRepository.GetAllAsync(request.pageNumber, request.pageSize);

            var result = _mapper.Map<IEnumerable<TeacherDto>>(teachers);

            _logger.LogInformation("Returned {Count} teachers from page {PageNumber}", result.Count(), request.pageNumber);

            return result;
        }
    }
}
