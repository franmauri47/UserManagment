using Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Common;

internal static class AuditableEntityConfiguration
{
    internal static void AddAuditableEntityConfiguration<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : AuditableEntity
    {
        builder.Property(e => e.CreationDate)
            .IsRequired();

        builder.Property(e => e.ModifiedDate);
    }
}
