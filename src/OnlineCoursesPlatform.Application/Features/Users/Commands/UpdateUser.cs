using MediatR;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Users.Dto;

namespace OnlineCoursesPlatform.Application.Features.Users.Commands
{
    public record UpdateUser(int Id, string UserName, string Email) : IRequest<UserDto?>;

    public class UpdateUserHandler : IRequestHandler<UpdateUser, UserDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto?> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (user == null) return null;

            user.UserName = request.UserName;
            user.Email = request.Email;

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            return UserDto.FromUser(user);
        }
    }
}
