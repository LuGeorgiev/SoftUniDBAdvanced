using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [StringLength(15,MinimumLength =3)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = new HashSet<CategoryProduct>();
    }
}
