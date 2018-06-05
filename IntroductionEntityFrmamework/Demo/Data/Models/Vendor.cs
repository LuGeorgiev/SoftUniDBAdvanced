
using System.Collections.Generic;

namespace Demo.Data.Models
{
    public  class Vendor
    {
        public Vendor()
        {             
        }

        public int VendorId { get; set; }
        public string Name { get; set; }

        public ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}
