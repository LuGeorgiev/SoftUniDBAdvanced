

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagraph.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250, MinimumLength =5)]
        public string Content  { get; set; }

        
        public int UserId  { get; set; }
        public virtual User User  { get; set; }

        
        public int PostId  { get; set; }
        public virtual Post Post  { get; set; }
    }
}
