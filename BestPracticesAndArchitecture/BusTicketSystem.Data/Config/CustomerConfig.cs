using System;
using System.Collections.Generic;
using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text;

namespace BusTicketsSystem.Data.Config
{
    class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.FirstName)
                .HasMaxLength(20)
                .IsUnicode(true);

            builder.Property(x => x.Gender)
                .HasMaxLength(12);

            builder.Property(x => x.LastName)
               .HasMaxLength(20)
               .IsUnicode(true);

            builder.Property(x => x.DateOfBirth)
               .HasColumnType("DATE");

            builder.HasOne(x => x.HomeTown)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.HomeTownId)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}
