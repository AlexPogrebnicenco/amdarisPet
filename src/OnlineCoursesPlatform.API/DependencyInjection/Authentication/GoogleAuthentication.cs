using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace OnlineCoursesPlatform.API.DependencyInjection.Authentication
{
    public static class GoogleAuthentication
    {
        public static IServiceCollection AddGoogleAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services
                 .AddAuthentication(options =>
                 {
                     options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                     options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                     options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // 🔥 это критично
                 })
                .AddCookie()
                .AddGoogle(options =>
                {
                    options.ClientId = config["Authentication:Google:ClientId"]!;
                    options.ClientSecret = config["Authentication:Google:ClientSecret"]!;
                    options.CallbackPath = "/signin-google";
                });

            return services;
        }
    }
}
