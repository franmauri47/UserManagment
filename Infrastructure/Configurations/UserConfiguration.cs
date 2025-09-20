using Domain.Entities;
using Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(320)
            .IsRequired();

        builder.AddAuditableEntityConfiguration<User>();
    }
}
