using DiscussionNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionNet.Persistence.Configurations
{
    public class CommentReactionConfiguration : IEntityTypeConfiguration<CommentReaction>
    {
        public void Configure(EntityTypeBuilder<CommentReaction> builder)
        {
            AuditableEntityConfiguration<CommentReaction>.SetProperties(builder);
            SoftDeleteConfiguration<CommentReaction>.SetProperties(builder);

            builder.Property(p => p.Reaction)
                .IsRequired()
                .HasConversion<byte>();

            builder.HasOne(m => m.Comment).WithMany(m => m.Reactions);
            builder.HasOne(m => m.ReactedBy).WithMany(m => m.Reactions);
        }
    }
}
