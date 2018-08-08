using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JsonCarDealer.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Sales = new List<Sale>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100,MinimumLength =3)]
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }

        public virtual ICollection<Sale> Sales{ get; set; }

    }
}
