using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Abstractions.Security;
using OnlineCoursesPlatform.Application.Features.Auth.Commands;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands.Handlers;

public class CreateUserFromGoogleHandler : IRequestHandler<CreateUserFromGoogle, AuthResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ILogger<CreateUserFromGoogleHandler> _logger;
    private readonly IMapper _mapper;

    public CreateUserFromGoogleHandler(
        IUnitOfWork unitOfWork,
        IJwtTokenGenerator jwtTokenGenerator,
        ILogger<CreateUserFromGoogleHandler> logger,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<AuthResponse> Handle(CreateUserFromGoogle request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var user = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);

        if (user == null)
        {
            user = _mapper.Map<User>(dto);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Google user created: {Email}", user.Email);
        }
        else
        {
            _logger.LogInformation("Google user already exists: {Email}", user.Email);
        }

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

        return new AuthResponse
        {
            AccessToken = accessToken,
            AccessTokenExpiration = DateTime.UtcNow.AddMinutes(60),
            RefreshToken = refreshToken,
            RefreshTokenExpiration = refreshTokenEntity.ExpiresAt,
            UserInfo = new UserAccountInfoDto
            {
                UserName = user.UserName,
                AvatarUrl = user.AvatarUrl
            }
        };
    }
}
