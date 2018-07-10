using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusTicketsSystem.Models
{
    public class Town
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Country { get; set; }

        public virtual ICollection<BusStation> BusStations { get; set; } = new HashSet<BusStation>();
        public virtual ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
        

    }
}
