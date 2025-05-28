using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Infrastructure.Persistence.Configurations
{
    public class ProgressRecordsConfig : IEntityTypeConfiguration<ProgressRecord>
    {
        public void Configure(EntityTypeBuilder<ProgressRecord> builder)
        {
            builder.HasKey(pr => new { pr.UserId, pr.LessonId});

            builder.Property(pr => pr.Completed)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(pr => pr.CompletedAt)
                .IsRequired(false);

            builder.HasOne(pr => pr.User)
                .WithMany(u => u.ProgressRecords)
                .HasForeignKey(pr => pr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pr => pr.Lesson)
                .WithMany(l => l.ProgressRecords)
                .HasForeignKey(pr => pr.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
