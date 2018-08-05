
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JsonCarDealer.Models
{
    public class Part
    {
        public Part()
        {
            this.PartCars = new List<PartCar>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = (3))]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal),"0","10000000")]
        public decimal Price { get; set; }

        [Required]
        [Range(0,1000000)]
        public int Quantity { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<PartCar> PartCars { get; set; }

    }
}
