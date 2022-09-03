using Eskisehirspor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eskisehirspor.Persistence.Configurations
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            AuditableEntityConfiguration<Topic>.SetProperties(builder);
            SoftDeleteConfiguration<Topic>.SetProperties(builder);

            builder.Property(p => p.Subject)
                 .IsRequired()
                 .HasMaxLength(Topic.SUBJECT_MAX_LENGTH);

            builder.Ignore(p => p.UrlName);
            builder.Ignore(p => p.Tags);
        }
    }
}
