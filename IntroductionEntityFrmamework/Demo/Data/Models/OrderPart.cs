using System;
using System.Collections.Generic;

namespace Demo.Data.Models
{
    public partial class OrderPart
    {

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int PartId { get; set; }
        public Part Part { get; set; }

        public int? Quantity { get; set; }
    }
}
