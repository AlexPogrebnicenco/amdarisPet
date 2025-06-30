using Microsoft.AspNetCore.Authentication.Cookies;
using OnlineCoursesPlatform.API.DependencyInjection.Authentication;

public static class AuthenticationConfigurationDI
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.LoginPath = "/api/auth/login";
            options.LogoutPath = "/api/auth/logout";
        });

        services.AddJwtAuthentication(config);

        return services;
    }
}
