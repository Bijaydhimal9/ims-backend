using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

/// <summary>
/// Represents the configuration for the <see cref="InmateProfile"/> entity
/// </summary>
public class InmateProfileConfiguration : IEntityTypeConfiguration<InmateProfile>
{
    /// <summary>
    /// Configures the entity
    /// </summary>
    /// <param name="builder">The builder to configure the entity. <see cref="EntityTypeBuilder{InmateProfile}"/>s</param>
    public void Configure(EntityTypeBuilder<InmateProfile> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.MiddleName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.DateOfBirth).IsRequired();
        builder.Property(x => x.CitizenshipNumber).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Gender).IsRequired().HasConversion<string>();
        builder.Property(x => x.Address).HasMaxLength(500);
        builder.Property(x => x.PhoneNumber).HasMaxLength(20);
        builder.Property(x => x.EmergencyContact).HasMaxLength(100);
        builder.Property(x => x.EmergencyContactPhone).HasMaxLength(20);

        builder.Property(x => x.CreatedBy).HasColumnType("VARCHAR(50)").HasMaxLength(50).IsRequired();
        builder.Property(x => x.CreatedOn).IsRequired().HasColumnType("DATETIME");
        builder.Property(x => x.UpdatedBy).HasColumnType("VARCHAR(50)").HasMaxLength(50).IsRequired(false);
        builder.Property(x => x.UpdatedOn).HasColumnType("DATETIME").IsRequired(false);

        builder.HasMany(x => x.Bookings).WithOne(b => b.InmateProfile).HasForeignKey(b => b.InmateId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.ApplicationUser).WithMany(u => u.Inmates).HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.CitizenshipNumber, x.DateOfBirth }).IsUnique();

    }
}