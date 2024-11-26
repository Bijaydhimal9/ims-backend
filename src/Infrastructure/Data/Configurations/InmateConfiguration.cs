using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class InmateConfiguration : IEntityTypeConfiguration<Inmate>
    {
        public void Configure(EntityTypeBuilder<Inmate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.MiddleName)
                .HasMaxLength(100);

            builder.Property(x => x.CitizenshipNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Address)
                .HasMaxLength(500);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20);
            builder.Property(x => x.EmergencyContact)
          .HasMaxLength(100);

            builder.Property(x => x.EmergencyContactPhone)
                .HasMaxLength(20);
            builder.HasIndex(x => new { x.CitizenshipNumber, x.DateOfBirth })
                .IsUnique();
            builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasColumnType("VARCHAR(50)").HasMaxLength(50).IsRequired();
            builder.Property(x => x.CreatedOn).HasColumnName("created_on").IsRequired().HasColumnType("DATETIME");
            builder.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasColumnType("VARCHAR(50)").HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.UpdatedOn).HasColumnName("updated_on").HasColumnType("DATETIME").IsRequired(false);
            // Configure relationships
            builder.HasMany(x => x.Bookings)
                .WithOne(b => b.Inmate)
                .HasForeignKey(b => b.InmateId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}