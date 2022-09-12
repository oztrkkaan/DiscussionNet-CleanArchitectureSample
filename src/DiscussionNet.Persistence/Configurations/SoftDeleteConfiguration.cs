using DiscussionNet.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionNet.Persistence.Configurations
{
    public static class SoftDeleteConfiguration<TEntity> where TEntity : class, ISoftDelete
    {
        public static EntityTypeBuilder<TEntity> SetProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(m => m.IsDeleted);

            builder.Property(m => m.DeletionDate);

            builder.HasQueryFilter(m => !m.IsDeleted);

            return builder;
        }
    }
}
