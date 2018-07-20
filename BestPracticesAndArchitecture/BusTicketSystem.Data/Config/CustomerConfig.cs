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

            //builder.HasData(
            //    new Customer {Id=1,BankAccountId=1,FirstName="Ivan",LastName="Ivanov", Gender="Male",HomeTownId=2},
            //    new Customer {Id=2,BankAccountId=2,FirstName="Petkan",LastName="Petkov", Gender="Male",HomeTownId=2},
            //    new Customer {Id=3,BankAccountId=3,FirstName="Martin",LastName="Martinov", Gender="Male",HomeTownId=2},
            //    new Customer {Id=4,BankAccountId=4,FirstName="Ivana",LastName="Ivanova", Gender="Male",HomeTownId=2},
            //    new Customer {Id=5,BankAccountId=5,FirstName="Marina",LastName="Marinova", Gender="Male",HomeTownId=2}
            //    );
        }
    }
}
