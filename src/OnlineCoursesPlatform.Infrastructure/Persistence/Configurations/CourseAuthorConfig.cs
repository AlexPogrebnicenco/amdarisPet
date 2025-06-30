using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Infrastructure.Persistence.Configurations
{
    public class CourseAuthorConfig : IEntityTypeConfiguration<CourseAuthor>
    {
        public void Configure(EntityTypeBuilder<CourseAuthor> builder)
        {
            builder.HasKey(ca => new { ca.CourseId, ca.UserId });

            builder.HasOne(ca => ca.Course)
                .WithMany(c => c.CourseAuthors)
                .HasForeignKey(ca => ca.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ca => ca.User)
                .WithMany(u => u.CourseAuthors)
                .HasForeignKey(ca => ca.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
