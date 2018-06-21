using Forum.Data;
using Forum.Models;
using Forum.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Forum.Services
{
    public class PostService : IPostService
    {
        private readonly ForumDbContext context;
        public PostService(ForumDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Post> All()
        {
            var posts = context.Posts
                .Include(p=>p.Author)
                .Include(p=>p.Category)
                .Include(p=>p.Replies)
                    .ThenInclude(r=>r.Author)
                .ToArray();

            return posts;
        }

        public Post Create( string title, string content, int categoryId, int authorId)
        {
            var post = new Post()
            {
                Title=title,
                Content=content,
                CategoryId=categoryId,
                AuthorId=authorId
            };

            context.Posts.Add(post);
            context.SaveChanges();

            return post;
        }

        public void Delete(int postId)
        {
            throw new NotImplementedException();
        }

        public Post FindById(int postId)
        {
            var post = context
                .Posts
                .Include(p=>p.Replies)
                    .ThenInclude(p=>p.Author)
                .SingleOrDefault(p=>p.Id==postId);

            return post;
        }
    }
}
