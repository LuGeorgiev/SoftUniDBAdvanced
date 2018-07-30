using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XMLProductShop.Models
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
        [StringLength(200, MinimumLength =3)]
        public string Name { get; set; }

        public int? BuyerId { get; set; }
        public virtual User Buyer { get; set; }

        [Required]
        [Range(typeof(decimal),"0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public int SellerId { get; set; }
        [Required]
        public virtual User Seller { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
