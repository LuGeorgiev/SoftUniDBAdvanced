using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
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
        [StringLength(50)]
        public string Make { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Required]
        [Range(0, 2147483647)]
        public int TravelledDistance { get; set; }        

        public virtual ICollection<PartCar> PartCars { get; set; }

    }
}
