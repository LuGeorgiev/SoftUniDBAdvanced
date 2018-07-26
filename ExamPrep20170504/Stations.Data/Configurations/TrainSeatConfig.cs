using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;
using Stations.Models.Enumerations;
using System;

namespace Stations.Data.Configurations
{
    public class TrainSeatConfig : IEntityTypeConfiguration<TrainSeat>
    {
        public void Configure(EntityTypeBuilder<TrainSeat> builder)
        {
            builder.HasOne(ts => ts.Train)
                 .WithMany(t => t.TrainSeats)
                 .HasForeignKey(ts => ts.TrainId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ts => ts.SeatingClass)
                .WithMany(sc => sc.TrainSeats)
                .HasForeignKey(ts => ts.SeatingClassId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
