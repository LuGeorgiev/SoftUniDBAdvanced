using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JsonProductShop.Models
{
    public class Product
    {
        public Product()
        {
            this.CategoryProducts = new List<CategoryProduct>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100,MinimumLength =3)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int SellerId { get; set; }
        public virtual User Seller { get; set; }

        public int? BuyerId { get; set; }
        public virtual User Buyer { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
