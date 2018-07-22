using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagraph.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Caption { get; set; }

        [Required]        
        public int UserId  { get; set; }        
        public virtual User User  { get; set; }

        [Required]        
        public int PictureId  { get; set; }
        public virtual Picture Picture  { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
