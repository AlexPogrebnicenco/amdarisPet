using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Abstractions.Security;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RegisterUserHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserHandler(IUnitOfWork unitOfWork, ILogger<RegisterUserHandler> logger, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Registration failed: Email already exists - {Email}", dto.Email);
                throw new InvalidOperationException("User with this email already exists.");
            }

            dto.Password = _passwordHasher.Hash(dto.Password);

            var user = _mapper.Map<User>(dto);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("User registered : {Email}", user.Email);
            return _mapper.Map<UserDto>(user);
        }
    }
}
