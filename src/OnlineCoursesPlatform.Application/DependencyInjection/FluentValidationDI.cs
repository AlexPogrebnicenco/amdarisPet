using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesPlatform.Application.Features.Auth.Dto.Validators;
using OnlineCoursesPlatform.Application.Features.Courses.Validators;
using OnlineCoursesPlatform.Application.Features.Users.Dto.Validators;

namespace OnlineCoursesPlatform.Application.DependencyInjection
{
    public static class FluentValidationDI
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateUserDtoValidator).Assembly);
            return services;
        }

    }
}
