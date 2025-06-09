namespace OnlineCoursesPlatform.API.DependencyInjection.Authentication
{
    public static class AuthenticationConfigurationDI
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddJwtAuthentication(config)
                .AddGoogleAuthentication(config);

            return services;
        }
    }
}
