using MediatR;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;

namespace OnlineCoursesPlatform.Application.Features.Users.Commands
{
    public record DeleteUser(int Id) : IRequest<bool>;

    public class DeleteUserHandler : IRequestHandler<DeleteUser, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (user == null) return false;

            await _unitOfWork.UserRepository.RemoveAsync(user);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}