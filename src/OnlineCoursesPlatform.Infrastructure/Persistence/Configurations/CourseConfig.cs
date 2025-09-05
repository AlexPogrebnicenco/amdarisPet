using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Enums;

namespace OnlineCoursesPlatform.Infrastructure.Persistence.Configurations
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(c => c.About)
                .HasMaxLength(4000)
                .HasDefaultValue("Nothing about this course");


            builder.Property(c => c.DateCreated)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(c => c.DateModified)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.HasOne(c => c.Category)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.Difficulty)
                 .IsRequired()
                 .HasConversion<string>()
                 .HasDefaultValue(CourseDifficulty.Beginner); ;
        }
    }
}
