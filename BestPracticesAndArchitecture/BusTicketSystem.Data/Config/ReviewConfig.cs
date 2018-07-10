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

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.BusCompany)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.BusCompanyId)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}
