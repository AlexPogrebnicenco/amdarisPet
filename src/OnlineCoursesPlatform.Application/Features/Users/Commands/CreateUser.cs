using MediatR;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Users.Commands
{
    public record CreateUser(string Name, string Email) : IRequest<UserDto>;

    public class CreateUserHandler : IRequestHandler<CreateUser, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.Name,
                Email = request.Email
            };

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return UserDto.FromUser(user);
        }
    }
}
