using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.App.Models
{
    // Mapping Objects Lecture
    public class PostDetailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string AuthorUsername { get; set; }

        public IEnumerable<ReplyDto> Replies { get; set; } = new List<ReplyDto>();

        public int ReplyCount { get; set; }
    }
}
