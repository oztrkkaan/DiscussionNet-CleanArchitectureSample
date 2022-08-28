using Eskisehirspor.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eskisehirspor.Persistence.Configurations
{
    public static class AuditableEntityConfiguration<TEntity> where TEntity : AuditableEntity
    {
        public static EntityTypeBuilder<TEntity> SetProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.CreationDate)
                .IsRequired();

            builder.Property(m => m.ModifiedDate);

            return builder;
        }
    }
}
