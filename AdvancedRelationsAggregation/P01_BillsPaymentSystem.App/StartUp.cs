using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Globalization;
using System.Linq;

namespace P01_BillsPaymentSystem.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            //P02 Seed
            //using (var db = new BillsPaymentSystemContext())
            //{
            //    //db.Database.Migrate();

            //    Seed(db);
            //}

            Console.WriteLine("Please, insert user Id");
            var userId =int.Parse(Console.ReadLine());

            //P03 User Details
            //ShowUserAccountsBalance(userId);


            //P04 PayBills
            Console.WriteLine("Please, enter amount needed to pay bills:");
            var amount = decimal.Parse(Console.ReadLine());
            PayBills(userId, amount);
            ShowUserAccountsBalance(userId);

        }

        private static void PayBills(int userId, decimal amount)
        {
            using (var db = new BillsPaymentSystemContext())
            {
                var toPayFromAccounts = db.BankAccounts
                    .Where(ba => ba.PaymentMethod.UserId == userId)
                    .OrderBy(ba=>ba.BankAccountId)
                    .ToList();

                var toPayFromCredit = db.CreditCards
                    .Where(cc => cc.PaymentMethod.UserId == userId)
                    .OrderBy(cc=>cc.CreditCardId)
                    .ToList();

                var totalBalance = toPayFromAccounts
                    .Sum(b => b.Balance) +
                    toPayFromCredit
                    .Sum(cc => cc.LimitLeft);

                if (totalBalance<amount)
                {
                    throw new ArgumentOutOfRangeException("Insufficient funds!");
                }

                bool wereBillsPaied = false;

                foreach (var bankAccount in toPayFromAccounts)
                {
                    if (wereBillsPaied)
                    {
                        break;
                    }

                    if (bankAccount.Balance>=amount)
                    {
                        bankAccount.Withdraw(amount);                            
                        amount = 0;
                        wereBillsPaied = true;
                    }
                    else
                    {
                        amount -= bankAccount.Balance;
                        bankAccount.Withdraw(bankAccount.Balance);
                    }
                }

                foreach (var creditCard in toPayFromCredit)
                {
                    if (wereBillsPaied)
                    {
                        break;
                    }

                    if (creditCard.LimitLeft>=amount)
                    {
                        creditCard.Withdraw(amount);
                        amount = 0;
                        wereBillsPaied = true;
                    }
                    else
                    {
                        amount -= creditCard.LimitLeft;
                        creditCard.Withdraw(creditCard.LimitLeft);
                    }

                }

                db.SaveChanges();
            }
        }

        private static void ShowUserAccountsBalance(int userId)
        {
            using (var db = new BillsPaymentSystemContext())
            {
                var user = db.Users
                    .Where(u => u.UserId == userId)
                    .Select(u => new
                    {
                        Name = u.FirstName + " " + u.LastName,

                        CreditCards = u.PaymentMethods
                       .Where(pm => pm.Type == PaymentMethodType.CreditCard)
                       .Select(pm => pm.CreditCard),

                        BankAccounts = u.PaymentMethods
                       .Where(pm => pm.Type == PaymentMethodType.BankAccount)
                       .Select(pm => pm.BankAccount)

                    })
                    .FirstOrDefault();
                if (user==null)
                {
                    throw new ArgumentException($"User wit Id {userId} was not found!");
                }

                Console.WriteLine($"User: {user.Name}");
                if (user.BankAccounts.Any()) //if at lease one exist
                {
                    Console.WriteLine("Bank Accounts:");
                    foreach (var ba in user.BankAccounts)
                    {
                        Console.WriteLine($"---ID:{ba.BankAccountId}");
                        Console.WriteLine($"---Balance:{ba.Balance}");
                        Console.WriteLine($"---Swift:{ba.SwiftCode}");
                    }
                }

                if (user.CreditCards.Any()) //if at lease one exist
                {
                    Console.WriteLine("Credit Cards:");
                    foreach (var cc in user.CreditCards)
                    {
                        Console.WriteLine($"---ID:{cc.CreditCardId}");
                        Console.WriteLine($"---MoneyOwned:{cc.MoneyOwned}");
                        Console.WriteLine($"---LimitLeft:{cc.LimitLeft}");
                        Console.WriteLine($"---Expiration Date {cc.ExpirationDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");
                    }
                }
            }
        }

        private static void Seed(BillsPaymentSystemContext db)
        {
            using ( db)
            {
                var user = new User()
                {
                    FirstName = "Pesho",
                    LastName = "Stamatov",
                    Email = "Pesho@abv.bg",
                    Password = "IamPesho"
                };
                               

                var credCards = new CreditCard[]
                {
                    new CreditCard()
                    {
                        ExpirationDate=new DateTime(2018,6,28),
                        Limit =1000m,
                        MoneyOwned = 5m
                    },

                    new CreditCard()
                    {
                        ExpirationDate=new DateTime(2012,4,12),
                        Limit =400m,
                        MoneyOwned = 200m
                    }
                };

                var bankAccount = new BankAccount()
                {
                    Balance = 1230m,
                    SwiftCode = "FINVBFSF",
                    BankName = "Swiss Band"
                };

                var paymentMethods = new PaymentMethod[]
                {
                    new PaymentMethod()
                    {
                        User=user,
                        CreditCard=credCards[0],
                        Type = PaymentMethodType.CreditCard
                    },
                    new PaymentMethod()
                    {
                        User=user,
                        CreditCard=credCards[1],
                        Type = PaymentMethodType.CreditCard
                    },
                    new PaymentMethod()
                    {
                        User=user,
                        BankAccount=bankAccount,
                        Type = PaymentMethodType.BankAccount
                    },
                };

                db.Users.Add(user);
                db.CreditCards.AddRange(credCards);
                db.BankAccounts.Add(bankAccount);
                db.PaymentMethods.AddRange(paymentMethods);

                db.SaveChanges();
            }
                
        }
    }
}
