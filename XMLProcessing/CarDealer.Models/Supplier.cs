
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
{
    public class Supplier
    {
        public Supplier()
        {
            this.Parts = new List<Part>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength =2)]
        public string Name { get; set; }

        public bool IsImporter { get; set; }

        public virtual ICollection<Part> Parts{ get; set; }
    }
}
