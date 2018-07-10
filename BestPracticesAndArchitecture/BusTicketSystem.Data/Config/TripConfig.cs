using System;
using System.Collections.Generic;
using System.Text;
using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketsSystem.Data.Config
{
    class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.Property(t => t.DepertureTime)
                .HasMaxLength(5)
                .IsUnicode(false);
            builder.Property(t => t.ArrivalTime)
                .HasMaxLength(5)
                .IsUnicode(false);

            builder.HasOne(t => t.BusCompany)
                .WithMany(b => b.Trips)
                .HasForeignKey(t => t.BusCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.OriginStation)
                .WithMany(b => b.OriginStationTrips)
                .HasForeignKey(t => t.OriginStationId)
                .OnDelete(DeleteBehavior.Restrict);
                

            builder.HasOne(t => t.DestinationStation)
                .WithMany(b => b.DestinationStationTrips)
                .HasForeignKey(t => t.DestinationStationId)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}
