using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(pm => pm.Id);

            //Keys cannot have NULL in them
            //builder.HasAlternateKey(pm => new {pm.CreditCardId, pm.BankAccountId,pm.UserId });

            //Have to be made with Index
            builder.HasIndex(pm => new { pm.CreditCardId, pm.BankAccountId, pm.UserId })
                .IsUnique();

            //One to one relation
            builder.HasOne(pm => pm.CreditCard)
                .WithOne(cc => cc.PaymentMethod)
                .HasForeignKey<PaymentMethod>(pm => pm.CreditCardId);

            builder.HasOne(pm => pm.BankAccount)
                .WithOne(cc => cc.PaymentMethod)
                .HasForeignKey<PaymentMethod>(pm => pm.BankAccountId);

            builder.HasOne(pm => pm.User)
                .WithMany(cc => cc.PaymentMethods)
                .HasForeignKey(pm => pm.UserId);
        }
    }
}
