using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesPlatform.Application.DependencyInjection;
using OnlineCoursesPlatform.Application.Features.Users.Dto.Validators;

public static class ApplicationDI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatRService();
        services.AddFluentValidation();
        services.AddAutoMapperServices();
        return services;
    }
}
