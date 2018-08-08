using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFood.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)] //Unique
        public string Name { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        [Required]
        [Range(typeof(decimal),"0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}