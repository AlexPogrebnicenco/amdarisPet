using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

public static class AuthenticationConfigurationDI
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.LoginPath = "/api/auth/login";
            options.LogoutPath = "/api/auth/logout";
        })
        .AddGoogle(options =>
        {
            options.ClientId = config["Authentication:Google:ClientId"]!;
            options.ClientSecret = config["Authentication:Google:ClientSecret"]!;
            options.CallbackPath = "/signin-google";
        });

        services.AddJwtAuthentication(config);

        return services;
    }
}
