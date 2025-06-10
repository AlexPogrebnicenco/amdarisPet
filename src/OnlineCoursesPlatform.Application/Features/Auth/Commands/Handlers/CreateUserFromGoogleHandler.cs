using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Abstractions.Security;
using OnlineCoursesPlatform.Application.Features.Auth.Commands;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Features.Auth.Commands.Handlers;

public class CreateUserFromGoogleHandler : IRequestHandler<CreateUserFromGoogle, AuthResultDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly ILogger<CreateUserFromGoogleHandler> _logger;
    private readonly IMapper _mapper;

    public CreateUserFromGoogleHandler(
        IUnitOfWork unitOfWork,
        IJwtTokenGenerator tokenGenerator,
        ILogger<CreateUserFromGoogleHandler> logger,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _tokenGenerator = tokenGenerator;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<AuthResultDto> Handle(CreateUserFromGoogle request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var user = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);

        if (user == null)
        {
            user = _mapper.Map<User>(dto);
            user.ExternalProvider = "Google";
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Google user created: {Email}", user.Email);
        }
        else
        {
            _logger.LogInformation("Google user already exists: {Email}", user.Email);
        }

        var now = DateTime.UtcNow;
        var accessTokenExpiration = now.AddMinutes(60);
        var refreshTokenExpiration = now.AddDays(7);

        var accessToken = _tokenGenerator.GenerateToken(user.Id, user.Email, user.UserName);
        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            ExpiresAt = refreshTokenExpiration,
            IsRevoked = false
        };

        await _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken);
        await _unitOfWork.SaveAsync();

        return new AuthResultDto
        {
            AccessToken = accessToken,
            AccessTokenExpiration = accessTokenExpiration,
            RefreshToken = refreshToken.Token,
            RefreshTokenExpiration = refreshToken.ExpiresAt,
            Email = user.Email,
            UserName = user.UserName
        };
    }
}
