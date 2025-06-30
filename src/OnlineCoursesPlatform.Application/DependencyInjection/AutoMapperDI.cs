using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesPlatform.Application.Mapping;

namespace OnlineCoursesPlatform.Application.DependencyInjection
{
    public static class AutoMapperDI
    {
        public static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile).Assembly);
            services.AddAutoMapper(typeof(TeacherRegistrationRequestProfile).Assembly);
            

            return services;
        }
    }
}
