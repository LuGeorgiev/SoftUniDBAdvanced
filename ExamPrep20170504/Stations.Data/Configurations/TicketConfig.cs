using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;
using Stations.Models.Enumerations;
using System;

namespace Stations.Data.Configurations 
{
    public class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasOne(t => t.Trip)
                .WithMany(tr => tr.Tickets)
                .HasForeignKey(t => t.TripId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.CustomerCard)
                .WithMany(c => c.BoughtTickets)
                .HasForeignKey(t => t.CustomerCardId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }

    }
}
