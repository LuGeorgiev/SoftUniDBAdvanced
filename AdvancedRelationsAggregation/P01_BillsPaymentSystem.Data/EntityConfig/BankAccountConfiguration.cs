using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(ba => ba.BankAccountId);

            builder.Ignore(ba => ba.PaymentMethodId);

            builder.Property(ba => ba.Balance)
                .IsRequired();

            builder.Property(ba => ba.BankName)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode();

            builder.Property(ba => ba.SwiftCode)
               .IsRequired()
               .HasMaxLength(20)
               .IsUnicode(false);
        }
    }
}
