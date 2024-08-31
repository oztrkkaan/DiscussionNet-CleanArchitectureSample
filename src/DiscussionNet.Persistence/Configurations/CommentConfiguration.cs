using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionNet.Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Domain.Entities.Comment>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Comment> builder)
        {
            AuditableEntityConfiguration<Domain.Entities.Comment>.SetProperties(builder);
            SoftDeleteConfiguration<Domain.Entities.Comment>.SetProperties(builder);

            builder.Property(m => m.Content)
                 .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<byte>();

            builder.Property(p => p.ParentCommentId);
            builder.Property(p => p.IpAddress)
                .IsRequired()
                .HasMaxLength(39);

            builder.HasOne(p => p.Topic).WithMany(p => p.Comments);
            builder.HasOne(p => p.User).WithMany(p => p.Comments);

            builder.Ignore(m => m.IsComment);
        }
    }
}
