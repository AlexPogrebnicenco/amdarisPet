using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Common.Exceptions;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Users.Commands.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUser, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating user with email: {Email}", request.Dto.Email);

            var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(request.Dto.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("User with email {Email} alreadt exists", request.Dto.Email);
                throw new ConflictException($"User with email '{request.Dto.Email}' already exists.");
            }

            var user = _mapper.Map<User>(request.Dto);
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("User created with ID: {UserId}", user.Id);

            return _mapper.Map<UserDto>(user);
        }
    }
}
