using System;
using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketsSystem.Data.Config
{
    class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(x => x.Content)
                .HasMaxLength(1000);

            builder.Property(x => x.PublishingDatetime)
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.BusCompany)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.BusCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasData(
            //    new Review { Id=1,CustomerId=1,Grade=2.3f,BusCompanyId=2, Content= "Auful trip" },
            //    new Review { Id=2,CustomerId=2,Grade=6.3f,BusCompanyId=2, Content= "Good trip" },
            //    new Review { Id=3,CustomerId=1,Grade=8.3f,BusCompanyId=2, Content= "GREATTTTT trip" },
            //    new Review { Id=4,CustomerId=3,Grade=2.9f,BusCompanyId=1, Content= "blah blah" },
            //    new Review { Id=5,CustomerId=4,Grade=9.3f,BusCompanyId=1, Content= "Nice trip" },
            //    new Review { Id=6,CustomerId=1,Grade=8.3f,BusCompanyId=2, Content= "Perfect trip" },
            //    new Review { Id=7,CustomerId=5,Grade=6.3f,BusCompanyId=4, Content= "Last trip" },
            //    new Review { Id=8,CustomerId=1,Grade=9.3f,BusCompanyId=2, Content= "I reccomend this trip" },
            //    new Review { Id=9,CustomerId=2,Grade=5.3f,BusCompanyId=3, Content= "It was OK" }
            //    );
        }
    }
}
