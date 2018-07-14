﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(cc => cc.CreditCardId);

            builder.Property(cc => cc.Limit)
                .IsRequired();

            builder.Property(cc => cc.MoneyOwned)
                .IsRequired();

            builder.Ignore(cc => cc.LimitLeft);

            //builder.Ignore(cc => cc.PaymentMethodId);

            builder.Property(cc => cc.ExpirationDate)
                .IsRequired();
        }
    }
}
