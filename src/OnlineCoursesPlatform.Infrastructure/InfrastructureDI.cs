using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Interfaces.Repositories;
using OnlineCoursesPlatform.Infrastructure.Persistence;
using OnlineCoursesPlatform.Infrastructure.Repositories;

public static class InfrastructureDI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=OnlineCoursesPlatformEF;Trusted_Connection=True;TrustServerCertificate=True"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICertificateRepository, CertificateRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseTagRepository, CourseTagRepository>();
        services.AddScoped<ICourseTeacherRepository, CourseTeacherRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<IProgressRecordRepository, ProgressRecordRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
   
        return services;
    }
}
