using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;

namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Commands.Handlers
{
    public class RejectTeacherRequestHandler : IRequestHandler<RejectTeacherRequestCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RejectTeacherRequestHandler> _logger;

        public RejectTeacherRequestHandler(IUnitOfWork unitOfWork, ILogger<RejectTeacherRequestHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(RejectTeacherRequestCommand request, CancellationToken cancellationToken)
        {
            var teacherRequest = await _unitOfWork.TeacherRegistrationRequestRepository.GetByIdAsync(request.RequestId);

            if (teacherRequest == null)
                throw new InvalidOperationException("Request not found.");

            teacherRequest.Status = "Rejected";
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Rejected teacher request: {Id}", request.RequestId);

            return Unit.Value;
        }
    }
}
