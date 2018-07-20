using BusTicketsSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusTicketsSystem.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [NotMapped]
        public Gender GenderEnum => (Gender)Enum.Parse(typeof(Gender), this.Gender.ToString());

        [Required]
        public string Gender { get; set; } //only this exists in DB as string


        public int? HomeTownId { get; set; }
        public virtual Town HomeTown { get; set; }

        [ForeignKey("BankAccount")]
        public int? BankAccountId { get; set; }
        public virtual BankAccount BankAccount { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
