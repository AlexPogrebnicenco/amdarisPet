using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Users.Dto;

namespace OnlineCoursesPlatform.Application.Features.Users.Queries.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsers, IEnumerable<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllUsersHandler> _logger;

        public GetAllUsersHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllUsersHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Requested list of users (page: {PageNumber}, size: {PageSize})", request.pageNumber, request.pageSize);
            var users = await _unitOfWork.UserRepository.GetAllAsync(request.pageNumber, request.pageSize);

            var result = _mapper.Map<IEnumerable<UserDto>>(users);

            _logger.LogInformation("Returned {Count} users from page {PageNumber}",result.Count(), request.pageNumber);
            return result;
        }
    }
}
