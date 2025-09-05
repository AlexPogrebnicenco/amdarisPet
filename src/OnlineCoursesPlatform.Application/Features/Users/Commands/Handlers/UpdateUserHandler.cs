using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Common.Exceptions;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using System.Security.Claims;

namespace OnlineCoursesPlatform.Application.Features.Users.Commands.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUser, UserAccountInfoDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateUserHandler> _logger;

        public UpdateUserHandler(IUnitOfWork unitOfWork, ILogger<UpdateUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<UserAccountInfoDto> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", request.UserId);
                throw new NotFoundException($"User with ID {request.UserId} not found.");
            }

            user.UserName = request.Dto.UserName;
            user.AvatarUrl = request.Dto.AvatarUrl;

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("User with ID {UserId} successfully updated their account.", request.UserId);

            return new UserAccountInfoDto
            {
                UserName = user.UserName,
                AvatarUrl = user.AvatarUrl
            };
        }
    }
}
