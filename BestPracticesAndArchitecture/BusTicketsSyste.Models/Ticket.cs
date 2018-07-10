using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusTicketsSystem.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int? Seat { get; set; }

        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int? TripId { get; set; }
        public virtual Trip Trip { get; set; }



    }
}
