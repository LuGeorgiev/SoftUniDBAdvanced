
using System.Collections.Generic;

namespace Demo.Data.Models
{
    public  class Part
    {
        public Part()
        {          
        }

        public int PartId { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int VendorId { get; set; }
        public int StockQty { get; set; }

        public Vendor Vendor { get; set; }
        public ICollection<OrderPart> OrderParts { get; set; } = new List<OrderPart>();
        public ICollection<PartNeeded> PartsNeeded { get; set; } = new List<PartNeeded>();
    }
}
