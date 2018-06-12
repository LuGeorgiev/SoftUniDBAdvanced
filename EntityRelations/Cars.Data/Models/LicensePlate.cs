using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Data.Models
{
    public class LicensePlate
    {
        public int? LicensePlateId { get; set; } // some car do not have license yet

        public string  Name { get; set; }

        public int? CarId { get; set; }
        public Car Car { get; set; }
    }
}
