using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Abstractions.Security;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, AuthResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RegisterUserHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterUserHandler(IUnitOfWork unitOfWork, ILogger<RegisterUserHandler> logger, IMapper mapper, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;

        }

        public async Task<AuthResponse> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Registration failed: Email already exists - {Email}", dto.Email);
                throw new InvalidOperationException("User with this email already exists.");
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                throw new InvalidOperationException("Passwords do not match.");
            }

            dto.Password = _passwordHasher.Hash(dto.Password);

            var user = _mapper.Map<User>(dto);
            user.Role = dto.Role;

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            var accessToken = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.UserName, user.Role);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            

            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshTokenEntity);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("User registered : {Email}", user.Email);
            return new AuthResponse
            {
                AccessToken = accessToken,
                AccessTokenExpiration = DateTime.UtcNow.AddMinutes(60),
                RefreshToken = refreshToken,
                RefreshTokenExpiration = refreshTokenEntity.ExpiresAt
            };
        }
    }
}
