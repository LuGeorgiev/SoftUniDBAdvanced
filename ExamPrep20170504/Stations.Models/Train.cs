using Stations.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stations.Models
{
    public class Train
    {
        public Train()
        {
            this.TrainSeats = new List<TrainSeat>();
            this.Trips = new List<Trip>();
        }
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string TrainNumber { get; set; } 

        public TrainType? Type { get; set; }

        public virtual ICollection<TrainSeat> TrainSeats { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

    }
}
