using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Common.Dto;
using OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Dto;

namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Queries.Handlers
{
    public class GetPendingTeacherRequestsHandler : IRequestHandler<GetPendingTeacherRequestsQuery, PagedResult<TeacherRequestDto>>
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

        public async Task<PagedResult<TeacherRequestDto>> Handle(GetPendingTeacherRequestsQuery request, CancellationToken cancellationToken)
        {
            var (items, totalCount) = await _unitOfWork.TeacherRegistrationRequestRepository
                .GetPendingRequestsAsync(request.Page, request.PageSize);

            _logger.LogInformation("Loaded {Count} pending teacher requests for page {Page}.", items.Count, request.Page);

            return new PagedResult<TeacherRequestDto>
            {
                Items = _mapper.Map<List<TeacherRequestDto>>(items),
                TotalCount = totalCount
            };
        }
    }
}
