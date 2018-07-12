using System;
using System.ComponentModel.DataAnnotations;

namespace Employees.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60)]
        public string LastName { get; set; }
        
        [Required]
        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }
                
        public string Address { get; set; }

    }
}
