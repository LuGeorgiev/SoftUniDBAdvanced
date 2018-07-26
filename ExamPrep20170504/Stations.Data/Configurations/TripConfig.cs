using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;
using Stations.Models.Enumerations;
using System;

namespace Stations.Data.Configurations
{
    public class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.Property(x => x.Status)
                .HasConversion(
                    v=>v.ToString(),
                    v=>(TripStatus)Enum.Parse(typeof(TripStatus),v,true))
                .HasDefaultValue(TripStatus.OnTime)
                .HasMaxLength(7);

            builder.HasOne(t => t.OriginStation)
                .WithMany(s => s.TripsFrom)
                .HasForeignKey(t => t.OriginStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.DestinationStation)
                .WithMany(s => s.TripsTo)
                .HasForeignKey(t => t.DestinationStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Train)
                .WithMany(train => train.Trips)
                .HasForeignKey(t => t.TrainId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
