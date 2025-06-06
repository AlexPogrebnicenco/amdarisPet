using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Teachers.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Teachers.Commands.Handlers
{
    public class UpdateTeacherHandler : IRequestHandler<UpdateTeacher, TeacherDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateTeacherHandler> _logger;

        public UpdateTeacherHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateTeacherHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TeacherDto?> Handle(UpdateTeacher request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating teacher with ID: {TeacherId}", request.Id);

            var teacher = await _unitOfWork.TeacherRepository.GetByIdAsync(request.Id);
            if (teacher == null)
            {
                _logger.LogWarning("Teacher with ID: {TeacherId} not found", request.Id);
                return null;
            }

            _mapper.Map(request.Dto, teacher);

            await _unitOfWork.TeacherRepository.UpdateAsync(teacher);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Teacher with ID: {TeacherId} was successfully updated", teacher.Id);

            return _mapper.Map<TeacherDto>(teacher);
        }
    }
}
