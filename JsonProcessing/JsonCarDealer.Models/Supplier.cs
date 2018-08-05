using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JsonCarDealer.Models
{
    public class Supplier
    {
        public Supplier()
        {
            this.Parts=new List<Part>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string  Name{ get; set; }
                
        public bool IsImporter { get; set; }

        public virtual ICollection<Part> Parts { get; set; }

    }
}
