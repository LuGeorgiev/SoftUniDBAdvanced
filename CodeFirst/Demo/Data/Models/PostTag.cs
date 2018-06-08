using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Data.Models
{
    public class PostTag
    {
        //For many to many relation
        [Key]
        public int PostId { get; set; }
        public Post Post { get; set; }

        [Key]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
