using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Data.Models
{
    public class Reply
    {
        public Reply()
        {

        }

        public Reply(string content, Post post, User autor)
        {
            this.Content = content;
            this.Post = post;
            this.Author = autor;
        }
        public int Id { get; set; }

        public string  Content { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public int AuthorId { get; set; }

        public User Author { get; set; }
    }
}
