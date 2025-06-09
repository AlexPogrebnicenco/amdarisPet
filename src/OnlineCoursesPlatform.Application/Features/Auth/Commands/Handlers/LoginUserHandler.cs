using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Abstractions.Security;
using OnlineCoursesPlatform.Application.Common.Exceptions;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUser, AuthResultDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<LoginUserHandler> _logger;

        public LoginUserHandler(
              IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator,
            ILogger<LoginUserHandler> logger
            )
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
        }

        public async Task<AuthResultDto> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var user = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                _logger.LogWarning("Login failed: user not found with email {Email}", dto.Email);
                throw new UnauthenticatedException("Invalid credentials");
            }

            if (!_passwordHasher.Verify(dto.Password, user.Password))
            {
                _logger.LogWarning("Login failed: invalid password for email {Email}", dto.Email);
                throw new UnauthenticatedException("Invalid credentials");
            }

            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.UserName);

            return new AuthResultDto
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(60),
            };
        }
    }
}
