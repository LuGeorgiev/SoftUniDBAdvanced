using Demo.Data;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Demo
{
    public class StartUp
    {
        static void Main(string[] args)
        {           
            var context = new ForumDbContext();

            RestartDatabase(context);

            //var categories = context.Categories
            //    .Include(c => c.Posts)
            //    .ThenInclude(p => p.Author)
            //    .Include(c=>c.Posts)
            //    .ThenInclude(p=>p.Replies)
            //    .ThenInclude(r=>r.Author)
            //    .ToArray();
            //foreach (var category in categories)
            //{
            //    Console.WriteLine($"{category.Name} ({category.Posts.Count})");
            //    foreach (var post in category.Posts)
            //    {
            //        Console.WriteLine($"--{post.Title}: {post.Content}");
            //        Console.WriteLine($"--by{post.Author.Username}");

            //        foreach (var repy in post.Replies)
            //        {
            //            Console.WriteLine($"----{repy.Content} from {repy.Author.Username}");
            //        }
            //    }
            //}

            // Another way
            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    Posts = c.Posts.Select(p=> new
                    {
                        Title = p.Title,
                        Content = p.Content,
                        AutorUsername = p.Author.Username,
                        Replies = p.Replies.Select(r=> new
                        {
                            Content=r.Content,
                            Author =r.Author.Username
                        })
                    })
                });

            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Name} ({category.Posts.Count()})");
                foreach (var post in category.Posts)
                {
                    Console.WriteLine($"--{post.Title}: {post.Content}");
                    Console.WriteLine($"--by{post.AutorUsername}");

                    foreach (var repy in post.Replies)
                    {
                        Console.WriteLine($"----{repy.Content} from {repy.Author}");
                    }
                }
            }
        }

        private static void RestartDatabase(ForumDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.Migrate();

            SeedDatabase(context);
        }

        private static void SeedDatabase(ForumDbContext context)
        {
            var users = new[]
            {
                new User("gosho","1234"),
                new User("IVAN","1234"),
                new User("Dragan","1234"),
                new User("Maria","1234")
            };
            context.Users.AddRange(users);

            var categories = new[]
            {
                new Category("CSharp"),
                new Category("JavaScript"),
                new Category("HTML"),
                new Category("SQL")
            };
            context.Categories.AddRange(categories);
            var posts = new[]
            {
                new Post("C# Rolz","Indeed",categories[0],users[0]),
                new Post("JS Rolz","Oh Yeas",categories[2],users[1]),
                new Post("Html is always needed","Unfortunately",categories[1],users[2]),
                new Post("SQL","Blah blah",categories[0],users[3]),
            };
            context.Posts.AddRange(posts);

            var reply = new[]
            {
                new Reply("I cannot speak" , posts[2],users[0])
            };
            context.Replies.AddRange(reply);


            context.SaveChanges();
        }
    }
}
