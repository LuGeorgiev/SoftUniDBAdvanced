using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductShop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]        
        public string FirstName { get; set; }

        public int? Age { get; set; }

        [InverseProperty("Seller")]
        public virtual ICollection<Product> ProductsForSale { get; set; } = new HashSet<Product>();

        [InverseProperty("Buyer")] 
        public virtual ICollection<Product> BoughtProducts { get; set; } = new HashSet<Product>();
    }
}
