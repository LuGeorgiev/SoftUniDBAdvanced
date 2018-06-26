using AutoMapper;
using Forum.App.Commands.Contracts;
using Forum.App.Models;
using Forum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forum.App.Commands
{
    public class PostDetailsCommand : ICommand
    {
        private readonly IPostService postService;

        public PostDetailsCommand(IPostService postService)
        {
            this.postService = postService;
        }
        public string Execute(params string[] args)
        {
            var postId = int.Parse(args[0]);
            //var post = postService.FindById(postId);

            //Automapper Refactoring
            var post = postService.FindById<PostDetailsDto>(postId);


            //mapping objects NOT manually
            //var postDto = Mapper.Map<PostDetailsDto>(post);

            //mapping objects part MANUALLY (not with automapper)
            //var postDto = new PostDetailsDto
            //{
            //    Id = post.Id,
            //    Title = post.Title,
            //    Content = post.Content,
            //    AuthorUsername = post.Author.Username,
            //    Replies = post.Replies.Select(r => new ReplyDto
            //    {
            //        Content = r.Content,
            //        AuthorUsername = r.Author.Username
            //    })
            //};

            var sb = new StringBuilder();

            sb.AppendLine($"{post.Title} by {post.AuthorUsername}");
            foreach (var repy in post.Replies)
            {
                sb.AppendLine($"   -{repy.Content} by {repy.AuthorUsername}");
            }

            // without DTOs
            //sb.AppendLine($"{post.Title} by {post.Author.Username}");
            //foreach (var repy in post.Replies)
            //{
            //    sb.AppendLine($"   -{repy.Content} by {repy.Author.Username}");
            //}

            return sb.ToString().TrimEnd();
        }
    }
}
