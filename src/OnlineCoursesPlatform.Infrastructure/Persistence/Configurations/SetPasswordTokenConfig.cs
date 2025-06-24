using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Infrastructure.Persistence.Configurations
{
    public class SetPasswordTokenConfig : IEntityTypeConfiguration<SetPasswordToken>
    {
        public void Configure(EntityTypeBuilder<SetPasswordToken> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Token)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(t => t.ExpiresAt)
                .IsRequired();

            builder.Property(t => t.IsUsed)
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany(u => u.SetPasswordTokens) 
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("SetPasswordTokens");
        }
    }
}
