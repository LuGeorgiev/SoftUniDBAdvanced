using System;
using System.Collections.Generic;

namespace Demo.Data.Models
{
    public class Order
    {
        public Order()
        {            
        }

        public int OrderId { get; set; }
        public int JobId { get; set; }
        public DateTime? IssueDate { get; set; }
        public bool? Delivered { get; set; }

        public Job Job { get; set; }
        public ICollection<OrderPart> OrderParts { get; set; } = new List<OrderPart>();
    }
}
