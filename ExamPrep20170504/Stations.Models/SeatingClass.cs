using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stations.Models
{
    public class SeatingClass
    {
        public SeatingClass()
        {
            this.TrainSeats = new List<TrainSeat>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; } //unique)

        [Required]
        [StringLength(2,MinimumLength =2)]
        public string Abbreviation { get; set; } //unique)

        public virtual ICollection<TrainSeat> TrainSeats { get; set; } //not according to specification
    }
}
