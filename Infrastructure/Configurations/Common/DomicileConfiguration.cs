using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Common;

internal class DomicileConfiguration : IEntityTypeConfiguration<Domicile>
{
    public void Configure(EntityTypeBuilder<Domicile> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.Property(e => e.Street)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.DirectionNumber)
            .HasMaxLength(10);

        builder.Property(e => e.City)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Province)
            .HasMaxLength(100)
            .IsRequired();

        builder.AddAuditableEntityConfiguration<Domicile>();

        builder.HasOne(d => d.User)
            .WithOne()
            .HasForeignKey<Domicile>(d => d.UserId)
            .IsRequired();
    }
}
