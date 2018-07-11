using BusTicketsSystem.Client.Core.Contracts;
using BusTicketsSystem.Data;
using BusTicketsSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketsSystem.Client.Core.Commands
{
    class BuyTicketCommand : ICommand
    {
        public string Execute(BusTicketsSystemContext db, string[] data)
        {
            int customreId = int.Parse(data[0]);
            int tripId = int.Parse(data[1]);
            decimal price = decimal.Parse(data[2]);
            int seatNumber = int.Parse(data[3]);

            var customer = db.Customers.Find(customreId);
            if (customer==null)
            {
                throw new ArgumentException($"Customer with ID: {customreId} do not exist in database!");
            }

            var trip = db.Trips.Find(tripId);
            if (trip==null)
            {
                throw new ArgumentException($"Trip with ID: {tripId} was not found!");
            }

            if (price<=0)
            {
                throw new ArgumentException("Invalid price");
            }

            var customerBalance = customer.BankAccount.Balance;
            if (customerBalance<price)
            {
                throw new InvalidOperationException($"Insufficient amount of money for customer {customer.FirstName} {customer.LastName} with bank account number {customer.BankAccount.AccountNumber}");
            }

            customer.BankAccount.Balance = -price;
            customer.Tickets.Add(new Ticket {Price=price,Seat=seatNumber, CustomerId=customreId, TripId=tripId});
            db.SaveChanges();

            return $"Customer {customer.FirstName} {customer.LastName} bought ticket for trip {tripId} for {price:F2} on seat {seatNumber}";
        }
    }
}
