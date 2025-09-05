using CloudinaryDotNet;

namespace OnlineCoursesPlatform.API.DependencyInjection.Cloudinary
{
    public static class CloudinaryDI
    {
        public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
        {
            var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]
            );

            var cloudinary = new CloudinaryDotNet.Cloudinary(account);
            services.AddSingleton(cloudinary);

            return services;
        }
    }
}
