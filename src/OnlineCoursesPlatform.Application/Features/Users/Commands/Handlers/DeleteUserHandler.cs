using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;

namespace OnlineCoursesPlatform.Application.Features.Users.Commands.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUser, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteUserHandler> _logger;

        public DeleteUserHandler(IUnitOfWork unitOfWork, ILogger<DeleteUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete user with ID: {UserId}", request.Id);

            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                _logger.LogWarning("User with ID: {UserId} not found", request.Id);
                return false;
            }

            await _unitOfWork.UserRepository.RemoveAsync(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("User with ID: {UserId} was successfully deleted", user.Id);

            return true;
        }
    }
}
