
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JsonCarDealer.Models
{
    public class Car
    {
        public Car()
        {
            this.PartCars = new List<PartCar>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Make { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Required]
        [Range(typeof(ulong),"0", "18446744073709551615")]
        public ulong TravelledDistance { get; set; }


        public virtual ICollection<PartCar> PartCars { get; set; }
    }
}
