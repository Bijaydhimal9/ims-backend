using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

/// <summary>
/// Represents the configuration for the <see cref="Booking"/> entity.
/// </summary>
public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    /// <summary>
    /// Configures the <see cref="Booking"/> entity.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.BookingNumber).HasColumnType("VARCHAR(20)").IsRequired();
        builder.Property(x => x.FacilityName).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(x => x.BookingLocation).HasColumnType("VARCHAR(200)").IsRequired();
        builder.Property(x => x.ReleaseDate).HasColumnType("DATETIME").IsRequired(false);
        builder.Property(x => x.ReleaseReason).HasColumnType("VARCHAR(500)").IsRequired(false);
        builder.Property(x => x.Comments).HasColumnType("VARCHAR(1000)").IsRequired(false);
        builder.Property(x => x.Status).IsRequired().HasConversion<string>();

        builder.Property(x => x.CreatedBy).HasColumnType("VARCHAR(50)").HasMaxLength(50).IsRequired();
        builder.Property(x => x.CreatedOn).IsRequired().HasColumnType("DATETIME");
        builder.Property(x => x.UpdatedBy).HasColumnType("VARCHAR(50)").HasMaxLength(50).IsRequired(false);
        builder.Property(x => x.UpdatedOn).HasColumnType("DATETIME").IsRequired(false);

        builder.HasOne(x => x.InmateProfile).WithMany(i => i.Bookings).HasForeignKey(x => x.InmateId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.ApplicationUser).WithMany(u => u.Bookings).HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.InmateId, x.Status }).IsUnique();
        builder.HasIndex(x => x.BookingNumber).IsUnique();

    }
}
