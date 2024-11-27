using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

/// <summary>
/// Represents the configuration for the <see cref="Charge"/> entity
/// </summary>
public class ChargeConfiguration : IEntityTypeConfiguration<Charge>
{
    /// <summary>
    /// Configures the entity
    /// </summary>
    /// <param name="builder">The builder to configure the entity. <see cref="EntityTypeBuilder{Charge}"/>s</param>
    public void Configure(EntityTypeBuilder<Charge> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ChargeName).HasMaxLength(200).IsRequired();
        builder.Property(x => x.ChargeCode).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.Status).IsRequired().HasConversion<string>();
        builder.Property(x => x.CreatedBy).HasColumnType("VARCHAR(50)").HasMaxLength(50).IsRequired();
        builder.Property(x => x.CreatedOn).IsRequired().HasColumnType("DATETIME");
        builder.Property(x => x.UpdatedBy).HasColumnType("VARCHAR(50)").HasMaxLength(50).IsRequired(false);
        builder.Property(x => x.UpdatedOn).HasColumnType("DATETIME").IsRequired(false);
        builder.HasOne(x => x.ApplicationUser).WithMany(u => u.Charges).HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Restrict);
    }
}
