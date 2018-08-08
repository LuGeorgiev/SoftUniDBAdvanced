using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Position
    {
        public Position()
        {
            this.Employees = new List<Employee>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)] // Unique
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

    }
}