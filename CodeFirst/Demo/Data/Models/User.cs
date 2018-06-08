﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Data.Models
{
    public class User
    {
        public User()
        {
        }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public ICollection<Reply> Replies { get; set; } = new List<Reply>();
    }
}
