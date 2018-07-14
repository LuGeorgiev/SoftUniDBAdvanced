using System;
using System.Collections.Generic;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }

        public decimal Balance { get; set; }

        public string BankName { get; set; }

        public string SwiftCode { get; set; }

        public int? PaymenMethodId { get; set; }
        public PaymentMethod PaymentMethod{ get; set; } 
        

        public void Withdraw(decimal amount)
        {
            if (this.Balance<amount||amount<0)
            {
                throw new ArgumentException("Insufficient amount");
            }

            this.Balance -= amount;
        }

        public void Deposite(decimal amount)
        {
            if (amount<0)
            {
                throw new ArgumentException("Amout have to be positive!");
            }

            this.Balance += amount;
        }
    }
}
