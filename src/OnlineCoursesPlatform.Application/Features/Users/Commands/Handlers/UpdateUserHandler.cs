using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Common.Exceptions;
using OnlineCoursesPlatform.Application.Features.Users.Dto;

namespace OnlineCoursesPlatform.Application.Features.Users.Commands.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUser, UserDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserHandler> _logger;

        public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto?> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating user with ID: {UserId}", request.Id);

            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                _logger.LogWarning("User with ID: {UserId} not found", request.Id);
                throw new NotFoundException($"User with ID {request.Id} not found");
            }

            _mapper.Map(request.Dto, user);

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("User with ID: {UserId} was successfully updated", user.Id);

            return _mapper.Map<UserDto>(user);
        }
    }
}
