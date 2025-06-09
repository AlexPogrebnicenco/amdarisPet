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
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Google user created: {Email}", user.Email);
        }
        else
        {
            _logger.LogInformation("Google user already exists: {Email}", user.Email);
        }

        var token = _tokenGenerator.GenerateToken(user.Id, user.Email, user.UserName);

        return new AuthResultDto
        {
            Token = token,
            Email = user.Email,
            UserName = user.UserName,
            Expiration = DateTime.UtcNow.AddMinutes(60)
        };
    }
}
