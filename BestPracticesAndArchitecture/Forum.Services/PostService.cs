using Forum.Data;
using Forum.Models;
using Forum.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Forum.Services
{
    public class PostService : IPostService
    {
        private readonly ForumDbContext context;
        public PostService(ForumDbContext context)
        {
            this.context = context;
        }

        public IQueryable<TModel> All<TModel>()
        {
            // with IEnumerable works without Automapper
            //var posts = context.Posts
            //    .Include(p => p.Author)
            //    .Include(p => p.Category)
            //    .Include(p => p.Replies)
            //        .ThenInclude(r => r.Author)
            //    .ToArray();

            var posts = context.Posts
                .ProjectTo<TModel>();
                

            return posts;
        }

        public TModel Create<TModel>( string title, string content, int categoryId, int authorId)
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

            //return post;

            var postDto = Mapper.Map<TModel>(post);
            return postDto;
        }

        public void Delete(int postId)
        {
            throw new NotImplementedException();
        }

        public TModel FindById<TModel>(int postId)
        {
            //var post = context
            //    .Posts
            //    .Include(p=>p.Author) 
            //    .Include(p=>p.Replies)
            //        .ThenInclude(p=>p.Author)
            //    .SingleOrDefault(p=>p.Id==postId);


            var post = context
                .Posts  
                .Where(p => p.Id == postId)
                .ProjectTo<TModel>()
                .SingleOrDefault();            
            
            return post;
        }
    }
}
