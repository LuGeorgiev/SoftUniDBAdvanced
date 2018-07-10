using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketsSystem.Data.Config
{
    class BusStationConfig : IEntityTypeConfiguration<BusStation>
    {
        public void Configure(EntityTypeBuilder<BusStation> builder)
        {
            builder.HasOne(x => x.Town)
                .WithMany(x => x.BusStations)
                .HasForeignKey(x => x.TownId);

            builder.Property(x => x.Name)
                .HasMaxLength(20);

            builder.HasMany(b => b.DestinationStationTrips)
                .WithOne(t => t.DestinationStation)
                .HasForeignKey(t => t.DestinationStationId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(b => b.OriginStationTrips)
                .WithOne(t => t.OriginStation)
                .HasForeignKey(t => t.OriginStationId)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}
