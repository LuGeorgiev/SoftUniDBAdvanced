using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XMLProductShop.Models
{
    public class User
    {
        public User()
        {
            this.ProductsToBuy = new List<Product>();
            this.ProductsToSell = new List<Product>();
        }

        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength =3)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public virtual ICollection<Product> ProductsToBuy { get; set; }
        public virtual ICollection<Product> ProductsToSell { get; set; }

    }
}
