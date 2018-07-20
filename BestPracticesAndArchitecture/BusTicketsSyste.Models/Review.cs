using BusTicketsSystem.Models.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusTicketsSystem.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [Grade]
        public float Grade { get; set; }

        public DateTime PublishingDatetime { get; set; }

        public int? BusCompanyId { get; set; }
        public virtual BusCompany BusCompany { get; set; }

        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
