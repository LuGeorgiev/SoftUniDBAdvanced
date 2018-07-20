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

            //builder.HasData(
            //    new Trip{Id=1,BusCompanyId=5,DestinationStationId=2,OriginStationId=4,ArrivalTime="13:56",DepertureTime="22:16" },
            //    new Trip{Id=2,BusCompanyId=4,DestinationStationId=5,OriginStationId=2,ArrivalTime="3:56",DepertureTime="13:16" },
            //    new Trip{Id=3,BusCompanyId=3,DestinationStationId=2,OriginStationId=5,ArrivalTime="13:56",DepertureTime="22:16" },
            //    new Trip{Id=4,BusCompanyId=2,DestinationStationId=3,OriginStationId=5,ArrivalTime="11:56",DepertureTime="15:06" },
            //    new Trip{Id=5,BusCompanyId=1,DestinationStationId=2,OriginStationId=5,ArrivalTime="12:56",DepertureTime="20:16" },
            //    new Trip{Id=6,BusCompanyId=3,DestinationStationId=4,OriginStationId=2,ArrivalTime="6:56",DepertureTime="23:16" },
            //    new Trip{Id=7,BusCompanyId=2,DestinationStationId=2,OriginStationId=1,ArrivalTime="13:56",DepertureTime="12:16" },
            //    new Trip{Id=8,BusCompanyId=4,DestinationStationId=5,OriginStationId=3,ArrivalTime="15:06",DepertureTime="2:16" }
            //    );
        }
    }
}
