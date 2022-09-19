using DiscussionNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DiscussionNet.Domain.Entities.User;

namespace DiscussionNet.Persistence.Configurations
{
    internal class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            AuditableEntityConfiguration<User>.SetProperties(builder);
            SoftDeleteConfiguration<User>.SetProperties(builder);

            builder.Property(m => m.Username)
                 .HasMaxLength(USERNAME_MAX_LENGTH)
                 .IsRequired();

            builder.Property(m => m.DisplayName)
                .HasMaxLength(DISPLAYNAME_MAX_LENGTH)
                .IsRequired();

            builder.Property(m => m.PasswordHash)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(m => m.PasswordSalt)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(m => m.Email)
                .HasMaxLength(EMAIL_MAX_LENGTH)
                .IsRequired();

            builder.Property(m => m.Location)
                .HasMaxLength(LOCATION_MAX_LENGTH);

            builder.Property(m => m.TimeOffsetHour)
                .HasMaxLength(SIGNATURE_MAX_LENGTH)
                .IsRequired();

            builder.Property(m => m.TimeOffset)
                .IsRequired();

            builder.Property(m => m.AuthorStatus)
                .IsRequired()
                .HasConversion<byte>();

            builder.Property(m => m.Roles)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.IsEmailVerified)
                .IsRequired();

            builder.HasMany(m => m.Notifications)
                .WithMany(m => m.ReceiverUsers)
                .UsingEntity(j => j.ToTable("UserNotification"));

        }
    }
}
