

using System;
using System.Collections.Generic;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Limit { get; set; }
        public decimal MoneyOwned { get; set; }

        public int? PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; } 

        public decimal LimitLeft
        {
            get
            {
               return this.Limit - this.MoneyOwned;
            }
        }        

        public void Deposite(decimal amount)
        {
            if (amount<0)
            {
                throw new ArgumentException("Amount have to be positive");
            }

            this.MoneyOwned -= amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount<0 || this.LimitLeft<amount)
            {
                throw new ArgumentException("Insufficient amount");
            }

            this.MoneyOwned += amount;
        }
    }
}
