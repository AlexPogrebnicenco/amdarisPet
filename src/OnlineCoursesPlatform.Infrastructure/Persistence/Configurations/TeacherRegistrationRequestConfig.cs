using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Enums;

public class TeacherRegistrationRequestConfig : IEntityTypeConfiguration<TeacherRegistrationRequest>
{
    public void Configure(EntityTypeBuilder<TeacherRegistrationRequest> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Age)
            .IsRequired();

        builder.Property(x => x.Gender)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Status)
       .HasConversion<string>()
       .IsRequired()
       .HasDefaultValue(TeacherRequestStatus.Pending);
    }
}
