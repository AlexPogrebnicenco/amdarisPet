using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Teachers.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Teachers.Commands.Handlers
{
    public class CreateTeacherHandler : IRequestHandler<CreateTeacher, TeacherDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateTeacherHandler> _logger;

        public CreateTeacherHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateTeacherHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TeacherDto> Handle(CreateTeacher request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating teacher with email: {Email}", request.Dto.Email);

            var teacher = _mapper.Map<Teacher>(request.Dto);
            await _unitOfWork.TeacherRepository.AddAsync(teacher);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Teacher created with ID: {TeacherId}", teacher.Id);

            return _mapper.Map<TeacherDto>(teacher);
        }
    }
}
