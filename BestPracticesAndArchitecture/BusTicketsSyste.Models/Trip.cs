using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusTicketsSystem.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[0-2]?[0-9]{1}:[0-5]{1}[0-9]{1}$",ErrorMessage ="Time pattern is h:MM")]
        public string DepertureTime { get; set; }

        [Required]
        [RegularExpression(@"^[0-2]?[0-9]{1}:[0-5]{1}[0-9]{1}$", ErrorMessage = "Time pattern is h:MM")]
        public string ArrivalTime { get; set; }

        [Required]
        public int OriginStationId { get; set; }
        public virtual BusStation OriginStation { get; set; }

        [Required]
        public int DestinationStationId { get; set; }
        public virtual BusStation DestinationStation { get; set; }

        [Required]
        public int BusCompanyId { get; set; }
        public virtual BusCompany BusCompany { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
