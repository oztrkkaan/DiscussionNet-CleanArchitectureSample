using DiscussionNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionNet.Persistence.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            AuditableEntityConfiguration<Notification>.SetProperties(builder);
            SoftDeleteConfiguration<Notification>.SetProperties(builder);

            builder.Property(m => m.Content)
                .IsRequired()
                .HasMaxLength(Notification.CONTENT_MAX_LENGTH);

            builder.Property(m => m.Url)
                .IsRequired()
                .HasMaxLength(Notification.URL_MAX_LENGTH);

        }
    }
}
