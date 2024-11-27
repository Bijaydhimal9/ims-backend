using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class BookingChargeConfiguration : IEntityTypeConfiguration<BookingCharge>
    {
        public void Configure(EntityTypeBuilder<BookingCharge> builder)
        {
            builder.HasKey(bc => bc.Id);
            builder.HasOne(bc => bc.Booking).WithMany().HasForeignKey(bc => bc.BookingId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(bc => bc.Charge).WithMany().HasForeignKey(bc => bc.ChargeId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.CreatedBy).HasColumnType("VARCHAR(50)").HasMaxLength(50).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired().HasColumnType("DATETIME");
            builder.Property(x => x.UpdatedBy).HasColumnType("VARCHAR(50)").HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.UpdatedOn).HasColumnType("DATETIME").IsRequired(false);
            builder.HasOne(x => x.ApplicationUser).WithMany(u => u.BookingCharges).HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Restrict);
        }
    }
}