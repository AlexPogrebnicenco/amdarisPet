using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Abstractions.Security;
using OnlineCoursesPlatform.Application.Interfaces.Repositories;
using OnlineCoursesPlatform.Infrastructure.Persistence;
using OnlineCoursesPlatform.Infrastructure.Repositories;
using OnlineCoursesPlatform.Infrastructure.Security;

public static class InfrastructureDI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options
                .UseLazyLoadingProxies() // Lazy Loading
                .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=OnlineCoursesPlatformEF;Trusted_Connection=True;TrustServerCertificate=True")
        );



        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICertificateRepository, CertificateRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseTagRepository, CourseTagRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<IProgressRecordRepository, ProgressRecordRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ITeacherRegistrationRequestRepository, TeacherRegistrationRequestRepository>();
        services.AddScoped<ISetPasswordTokenRepository, SetPasswordTokenRepository>();


        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddScoped<ICourseAuthorRepository, CourseAuthorRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();



        return services;
    }
}
