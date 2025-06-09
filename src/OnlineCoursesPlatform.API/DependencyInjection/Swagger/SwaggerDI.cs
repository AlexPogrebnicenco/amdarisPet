using Microsoft.OpenApi.Models;
using OnlineCoursesPlatform.API.Swagger;

namespace OnlineCoursesPlatform.API.DependencyInjection.Swagger
{
    public static class SwaggerDI
    {
        public static IServiceCollection AddSwaggerWithCustomOptions(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // Подключение кастомных фильтров
                options.OperationFilter<AddAcceptHeaderOperationFilter>();

                // Настройки для JWT авторизации (если нужно)
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}
