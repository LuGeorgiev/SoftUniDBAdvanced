using BusTicketsSystem.Client.Core.Contracts;
using BusTicketsSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusTicketsSystem.Client.Core.Commands
{
    class PrintReviewCommand : ICommand
    {
        public string Execute(BusTicketsSystemContext db, string[] data)
        {
            var companyId = int.Parse(data[0]);
            var company = db.BusCompanies
                .FirstOrDefault(x => x.Id == companyId);
            if (company==null)
            {
                throw new ArgumentException($"Company {companyId} was not found!");
            }

            var sb = new StringBuilder();
            foreach (var review in company.Reviews)
            {
                sb.AppendLine($"{review.Id} {review.Grade:F2} {review.PublishingDatetime}");
                sb.AppendLine($"{review.Customer.FirstName} {review.Customer.LastName}");
                sb.AppendLine($"{review.Content}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
