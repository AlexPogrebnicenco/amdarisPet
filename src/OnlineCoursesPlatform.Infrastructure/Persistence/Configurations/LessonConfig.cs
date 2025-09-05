using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Infrastructure.Persistence.Configurations
{
    public class LessonConfig : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.OrderNumber)
                .IsRequired();

            builder.Property(x => x.Content)
                .IsRequired();

            var videoInfoProperty = builder.Property(x => x.VideoUrls)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<VideoInfo>>(v, (JsonSerializerOptions?)null) ?? new()
                );

            videoInfoProperty.Metadata.SetValueComparer(new ValueComparer<List<VideoInfo>>(
                (c1, c2) => (c1 ?? new()).SequenceEqual(c2 ?? new()),
                c => c != null ? c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Url.GetHashCode(), v.Title.GetHashCode())) : 0,
                c => c == null ? new() : c.ToList()
            ));

            // Хранение списка ресурсов как JSON
            var resourcesProperty = builder.Property(x => x.Resources)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
                ); 

            resourcesProperty.Metadata.SetValueComparer(new ValueComparer<List<string>>(
                (c1, c2) => (c1 ?? new List<string>()).SequenceEqual(c2 ?? new List<string>()),
                c => (c != null ? c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())) : 0),
                c => c == null ? new List<string>() : c.ToList()
            ));


            builder.HasOne(x => x.Course)
                .WithMany(c => c.Lessons)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Уникальный индекс на пару (CourseId + OrderNumber)
            builder.HasIndex(x => new { x.CourseId, x.OrderNumber })
                .IsUnique();
        }
    }
}
