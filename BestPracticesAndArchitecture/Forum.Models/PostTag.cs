using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Forum.Models
{
    public class PostTag
    {
        [Key]
        public int PostId { get; set; }
        public Post Post { get; set; }

        [Key]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
