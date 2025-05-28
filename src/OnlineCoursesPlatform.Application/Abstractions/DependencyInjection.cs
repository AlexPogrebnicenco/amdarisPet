using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesPlatform.Application.Features.Users.Commands;

public static class ApplicationDI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(CreateUser).Assembly);

        return services;
    }
}
