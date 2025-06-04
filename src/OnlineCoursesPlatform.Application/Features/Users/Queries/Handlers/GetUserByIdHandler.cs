using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Users.Dto;

namespace OnlineCoursesPlatform.Application.Features.Users.Queries.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserById, UserDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserByIdHandler> _logger;

        public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetUserByIdHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto?> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Requested user with ID : {UserId}", request.Id);
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);

            if (user == null) 
            {
                _logger.LogWarning("User with ID: {UserId} not found", request.Id);
                return null;
            }

            _logger.LogInformation("User with ID: {UserId} was found", user.Id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }
    }
}
