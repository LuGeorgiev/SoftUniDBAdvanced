
using System.Collections.Generic;

namespace Cars.Data.Models
{
    public class Dealership
    {
        public int DealershipId { get; set; }

        public string Name { get; set; }

        public ICollection<CarDealership> CarDealerships { get; set; } = new List<CarDealership>();
    }
}
