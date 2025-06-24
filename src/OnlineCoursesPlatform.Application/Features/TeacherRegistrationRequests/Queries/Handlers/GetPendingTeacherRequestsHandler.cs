using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Dto;

namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Queries.Handlers
{
    public class GetPendingTeacherRequestsHandler : IRequestHandler<GetPendingTeacherRequestsQuery, List<TeacherRequestDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPendingTeacherRequestsHandler> _logger;

        public GetPendingTeacherRequestsHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetPendingTeacherRequestsHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<TeacherRequestDto>> Handle(GetPendingTeacherRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _unitOfWork.TeacherRegistrationRequestRepository.GetPendingRequestsAsync();

            _logger.LogInformation("Loaded {Count} pending teacher requests.", requests.Count);

            return _mapper.Map<List<TeacherRequestDto>>(requests);
        }
    }
}
