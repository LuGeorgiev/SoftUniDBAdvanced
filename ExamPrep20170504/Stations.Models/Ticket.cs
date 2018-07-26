﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stations.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(typeof(decimal),"0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]        
        [RegularExpression(@"^[A-Z]{2}\d{1,6}$")]
        public string SeatingPlace { get; set; } 

        [Required]
        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }

        public int? CustomerCardId { get; set; } 
        public virtual CustomerCard CustomerCard { get; set; }

    }
}
