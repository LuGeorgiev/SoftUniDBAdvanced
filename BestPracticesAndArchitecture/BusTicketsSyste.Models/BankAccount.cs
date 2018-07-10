using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusTicketsSystem.Models
{
    public class BankAccount
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
