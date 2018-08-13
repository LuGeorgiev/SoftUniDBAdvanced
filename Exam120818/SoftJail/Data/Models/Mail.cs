using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SoftJail.Data.Models
{
    public class Mail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description  {get; set;}

        [Required]
        public string Sender  {get; set;}

        [Required]
        [RegularExpression(@"^[0-9a-zA-Z\ ]+str\.$")]
        public string Address  {get; set;}

        [ForeignKey("Prisoner")]
        public int PrisonerId {get; set;} 

        [Required]
        public virtual Prisoner Prisoner  {get; set;}

    }
}
