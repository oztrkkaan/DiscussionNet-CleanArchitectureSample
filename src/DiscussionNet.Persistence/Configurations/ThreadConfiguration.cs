using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionNet.Persistence.Configurations
{
    public class ThreadConfiguration : IEntityTypeConfiguration<Domain.Entities.Thread>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Thread> builder)
        {
            AuditableEntityConfiguration<Domain.Entities.Thread>.SetProperties(builder);
            SoftDeleteConfiguration<Domain.Entities.Thread>.SetProperties(builder);

            builder.Property(m => m.Content)
                 .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<byte>();

            builder.Property(p => p.ParentThreadId);
            builder.Property(p => p.IpAddress)
                .IsRequired()
                .HasMaxLength(39);

            builder.HasOne(p => p.Topic).WithMany(p => p.Threads);
            builder.HasOne(p => p.User).WithMany(p => p.Threads);

            builder.Ignore(m => m.LikeCount);
            builder.Ignore(m => m.UnlikeCount);
            builder.Ignore(m => m.IsComment);
        }
    }
}
