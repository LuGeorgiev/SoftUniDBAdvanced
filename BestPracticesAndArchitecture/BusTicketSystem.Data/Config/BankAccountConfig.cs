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

            builder.Property(x => x.Balance)
                .HasDefaultValue(0);

            builder.Property(x => x.AccountNumber)
                .HasMaxLength(15)
                .IsUnicode(false);

            //builder.HasData(
            //    new BankAccount {Id=1,AccountNumber ="3456456854",Balance=5000,CustomerId=1 },
            //    new BankAccount {Id=2,AccountNumber ="568926232",Balance=200,CustomerId=2 },
            //    new BankAccount {Id=3,AccountNumber ="4621122324",CustomerId=3 },
            //    new BankAccount {Id=4,AccountNumber ="659532413",Balance=2000,CustomerId=4 },
            //    new BankAccount {Id=5,AccountNumber ="235643232",Balance=160,CustomerId=5 }
            //    );
        }
    }
}
