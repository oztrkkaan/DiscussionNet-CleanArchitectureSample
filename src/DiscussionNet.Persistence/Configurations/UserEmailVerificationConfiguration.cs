using DiscussionNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionNet.Persistence.Configurations
{
    public class UserEmailVerificationConfiguration : IEntityTypeConfiguration<UserEmailVerification>
    {
        public void Configure(EntityTypeBuilder<UserEmailVerification> builder)
        {
            AuditableEntityConfiguration<UserEmailVerification>.SetProperties(builder);

            builder.Property(m => m.Guid)
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(m => m.ExpirationDate)
                .IsRequired();

            builder.Property(m => m.IsValid)
                .IsRequired();

            builder.HasOne(m => m.User).WithMany(m => m.EmailVerifications);

            builder.Ignore(m => m.IsExpired);
        }
    }
}
