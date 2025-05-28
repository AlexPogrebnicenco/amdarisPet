using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Infrastructure.Persistence.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CategoryName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(c => c.CategoryName)
                .IsUnique();
        }
    }
}
