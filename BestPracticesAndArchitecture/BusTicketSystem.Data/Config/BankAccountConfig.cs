using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketsSystem.Data.Config
{
    internal class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasOne(x => x.Customer)
                .WithOne(x => x.BankAccount)
                .HasForeignKey<BankAccount>(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Balance)
                .HasDefaultValue(0);

            builder.Property(x => x.AccountNumber)
                .HasMaxLength(15)
                .IsUnicode(false);

        }
    }
}
