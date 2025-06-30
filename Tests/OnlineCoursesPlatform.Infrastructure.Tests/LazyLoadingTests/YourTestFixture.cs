using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace OnlineCoursesPlatform.Infrastructure.Tests.LazyLoadingTests
{
    public class YourTestFixture : IDisposable
    {
        public AppDbContext Context { get; private set; }

        public YourTestFixture()
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
                options
                    .UseLazyLoadingProxies() // Включаем Lazy Loading
                    .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OnlineCoursesPlatformEF;Trusted_Connection=True;TrustServerCertificate=True")
            );

            var serviceProvider = services.BuildServiceProvider();

            Context = serviceProvider.GetRequiredService<AppDbContext>();

            // Можно подготовить базу: миграции и seed (если нужно)
            Context.Database.Migrate();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
