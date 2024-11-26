using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BookingNumber).HasMaxLength(20).IsRequired();
            builder.Property(x => x.FacilityName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.BookingLocation).HasMaxLength(200).IsRequired();
            builder.Property(x => x.ReleaseReason).HasMaxLength(500);
            builder.Property(x => x.Comments).HasMaxLength(1000);
            builder.HasIndex(x => x.BookingNumber).IsUnique();
            builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasColumnType("VARCHAR(50)").HasMaxLength(50).IsRequired();
            builder.Property(x => x.CreatedOn).HasColumnName("created_on").IsRequired().HasColumnType("DATETIME");
            builder.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasColumnType("VARCHAR(50)").HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.UpdatedOn).HasColumnName("updated_on").HasColumnType("DATETIME").IsRequired(false);
            builder.HasOne(x => x.InmateProfile).WithMany(i => i.Bookings).HasForeignKey(x => x.InmateId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ApplicationUser).WithMany(u => u.Bookings).HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(x => new { x.InmateId, x.Status }).IsUnique();
        }
    }
}