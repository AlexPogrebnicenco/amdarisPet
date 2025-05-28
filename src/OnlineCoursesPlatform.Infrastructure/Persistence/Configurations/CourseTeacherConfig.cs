using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Infrastructure.Persistence.Configurations
{
    public class CourseTeacherConfig : IEntityTypeConfiguration<CourseTeacher>
    {
        public void Configure(EntityTypeBuilder<CourseTeacher> builder)
        {
            builder.HasKey(ct => new { ct.CourseId, ct.TeacherId });

            builder.HasOne(ct => ct.Course)
                .WithMany(c => c.CourseTeachers)
                .HasForeignKey(ct => ct.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ct => ct.Teacher)
                .WithMany(t => t.CourseTeachers)
                .HasForeignKey(ct => ct.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
