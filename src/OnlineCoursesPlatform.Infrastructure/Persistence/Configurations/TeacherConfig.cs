using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Infrastructure.Persistence.Configurations
{
    public class TeacherConfig : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.TeacherName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(t => t.Email)
                .IsUnique();
        }
    }
}
