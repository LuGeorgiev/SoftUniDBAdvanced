using BusTicketsSystem.Client.Core.Contracts;
using BusTicketsSystem.Data;
using BusTicketsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusTicketsSystem.Client.Core.Commands
{
    class PublishReviewCommand : ICommand
    {
        public string Execute(BusTicketsSystemContext db, string[] data)
        {
            var customerId = int.Parse(data[0]);
            var grade = float.Parse(data[1]);
            var busCompName = data[2];
            var content = data[3];

            var customer = db.Customers
                .FirstOrDefault(x => x.Id == customerId);
            if (customer == null)
            {
                throw new ArgumentException($"Customer with Id: {customerId} was not found!");
            }

            var busCompany = db.BusCompanies
                .FirstOrDefault(x => x.Name == busCompName);
            if (busCompany==null)
            {
                throw new ArgumentException($"Company: {busCompName} was not found!");
            }

            db.Reviews.Add(new Review {Content=content,Grade=grade, BusCompanyId=busCompany.Id, CustomerId=customerId });
            db.SaveChanges();

            string lastName = customer.LastName;
            if (lastName.EndsWith("s"))
            {
                lastName += "'";
            }
            else
            {
                lastName += "'s";
            }

            return $"{customer.FirstName} {lastName} review was successfully published";
        }
    }
}
