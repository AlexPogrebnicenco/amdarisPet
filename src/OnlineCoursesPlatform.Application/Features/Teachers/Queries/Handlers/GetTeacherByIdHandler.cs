using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Teachers.Dto;

namespace OnlineCoursesPlatform.Application.Features.Teachers.Queries.Handlers
{
    public class GetTeacherByIdHandler : IRequestHandler<GetTeacherById, TeacherDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetTeacherByIdHandler> _logger;

        public GetTeacherByIdHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GetTeacherByIdHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TeacherDto?> Handle(GetTeacherById request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Requested teacher with ID: {TeacherId}", request.Id);

            var teacher = await _unitOfWork.TeacherRepository.GetByIdAsync(request.Id);

            if (teacher == null)
            {
                _logger.LogWarning("Teacher with ID: {TeacherId} not found", request.Id);
                return null;
            }

            _logger.LogInformation("Teacher with ID: {TeacherId} was found", teacher.Id);
            return _mapper.Map<TeacherDto>(teacher);
        }
    }
}
