using Forum.App.Commands.Contracts;
using Forum.Services.Contracts;
using System;
using System.Collections.Generic;
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
            var post = postService.FindById(postId);

            var sb = new StringBuilder();

            sb.AppendLine($"{post.Title} by {post.Author.Username}");
            foreach (var repy in post.Replies)
            {
                sb.AppendLine($"   -{repy.Content} by {repy.Author.Username}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
