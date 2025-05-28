using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Certificate> Certificates => Set<Certificate>();
        public DbSet<ProgressRecord> ProgressRecords => Set<ProgressRecord>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<CourseTag> CourseTags => Set<CourseTag>();
        public DbSet<CourseTeacher> CourseTeachers => Set<CourseTeacher>();

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if(!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder
        //            .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=OnlineCoursesPlatformEF;Trusted_Connection=True;TrustServerCertificate=True")
        //            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
        //                LogLevel.Information);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
