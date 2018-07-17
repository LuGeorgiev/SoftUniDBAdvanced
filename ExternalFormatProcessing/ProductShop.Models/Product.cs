using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0,100000000)]
        public decimal Price { get; set; }

        public int? BuyerId { get; set; }
        public virtual User Buyer { get; set; }

        public int SellerId { get; set; }
        public virtual User Seller { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = new HashSet<CategoryProduct>();
    }
}
