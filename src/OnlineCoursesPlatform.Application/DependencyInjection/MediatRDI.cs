using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesPlatform.Application.Features.Users.Commands;

namespace OnlineCoursesPlatform.Application.DependencyInjection
{
    public static class MediatRDI
    {
        public static IServiceCollection AddMediatRService(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateUser).Assembly);
            });

            return services;
        }
    }
}
