using System;
using System.Collections.Generic;
using System.Text;
using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketsSystem.Data.Config
{
    class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(x => x.Price)
                .IsRequired();

            builder.HasOne(t => t.Customer)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Trip)
                .WithMany(tr => tr.Tickets)
                .HasForeignKey(t => t.TripId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasData(
            //    new Ticket {Id=1,TripId=1,CustomerId=2,Price=98.76m,Seat=2 },
            //    new Ticket {Id=2,TripId=2,CustomerId=1,Price=34,Seat=1 },
            //    new Ticket {Id=3,TripId=3,CustomerId=3,Price=9.76m,Seat=3 },
            //    new Ticket {Id=4,TripId=4,CustomerId=2,Price=89.76m,Seat=1 },
            //    new Ticket {Id=5,TripId=1,CustomerId=1,Price=58.76m,Seat=1 },
            //    new Ticket {Id=6,TripId=2,CustomerId=5,Price=9.76m,Seat=1 },
            //    new Ticket {Id=7,TripId=1,CustomerId=4,Price=34.76m,Seat=2 },
            //    new Ticket {Id=8,TripId=3,CustomerId=2,Price=23.76m,Seat=4 },
            //    new Ticket {Id=9,TripId=4,CustomerId=3,Price=16.76m,Seat=1 }
            //    );
        }
    }
}
