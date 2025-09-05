using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesPlatform.Application.Abstractions.Services;
using OnlineCoursesPlatform.Application.DependencyInjection;
using OnlineCoursesPlatform.Application.Features.Users.Dto.Validators;
using OnlineCoursesPlatform.Application.Services.Email;
using OnlineCoursesPlatform.Application.Settings;
using Microsoft.Extensions.Configuration;
using MediatR;
using OnlineCoursesPlatform.Application.Common.Behaviors;


public static class ApplicationDI
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatRService();
        services.AddFluentValidation();
        services.AddAutoMapperServices();

        // Connect EmailSettings
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        // Connect FrontendSettings
        services.Configure<FrontendSettings>(configuration.GetSection("FrontendSettings"));

        // Connect GmailEmailService
        services.AddScoped<IEmailService, GmailEmailService>();

        // Register pipeline for transactions
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        return services;
    }
}
