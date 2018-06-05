using System;
using System.Collections.Generic;

namespace Demo.Data.Models
{
    public class Job
    {
        public Job()
        {           
        }

        public int JobId { get; set; }
        public string Status { get; set; }
        public int ModelId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? FinishDate { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int? MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }

        public PartModel Model { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<PartNeeded> PartsNeeded { get; set; } = new List<PartNeeded>();
    }
}
