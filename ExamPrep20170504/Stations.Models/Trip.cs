using Stations.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stations.Models
{
    public class Trip
    {
        public Trip()
        {
            this.Tickets = new List<Ticket>();
        }

        [Key]
        public int Id { get; set; }

        public int OriginStationId { get; set; }
        [Required]
        public virtual Station OriginStation { get; set; }

        public int DestinationStationId { get; set; }
        [Required]
        public virtual Station DestinationStation { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }  // TODO to be after DepartureTime

        public int TrainId { get; set; }
        [Required]
        public virtual Train Train { get; set; }

        public TripStatus Status { get; set; } 
                
        public TimeSpan? TimeDifference {get; set;}

        public virtual ICollection<Ticket> Tickets { get; set; } //not according to specification

    }
}