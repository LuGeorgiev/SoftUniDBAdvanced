using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusTicketsSystem.Models
{
    public class BusStation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        public int? TownId { get; set; }
        public virtual Town Town{ get; set; }

        public virtual ICollection<Trip> OriginStationTrips { get; set; } = new HashSet<Trip>();
        public virtual ICollection<Trip> DestinationStationTrips { get; set; } = new HashSet<Trip>();
    }
}
