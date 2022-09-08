using Eskisehirspor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eskisehirspor.Persistence.Configurations
{
    public class ThreadReactionConfiguration : IEntityTypeConfiguration<ThreadReaction>
    {
        public void Configure(EntityTypeBuilder<ThreadReaction> builder)
        {
            AuditableEntityConfiguration<ThreadReaction>.SetProperties(builder);
            SoftDeleteConfiguration<ThreadReaction>.SetProperties(builder);

            builder.Property(p => p.Reaction)
                .IsRequired()
                .HasConversion<byte>();

            builder.HasOne(m => m.Thread).WithMany(m => m.Reactions);
            builder.HasOne(m => m.ReactedBy).WithMany(m => m.Reactions);
        }
    }
}
