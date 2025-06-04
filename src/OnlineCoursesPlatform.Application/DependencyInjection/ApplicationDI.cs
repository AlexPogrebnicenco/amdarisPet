using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesPlatform.Application.DependencyInjection;
using OnlineCoursesPlatform.Application.Features.Users.Commands;

public static class ApplicationDI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatRService();
        services.AddAutoMapperServices();
        return services;
    }
}
