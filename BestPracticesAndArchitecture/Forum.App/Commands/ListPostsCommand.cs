using Forum.App.Commands.Contracts;
using Forum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forum.App.Commands
{
    public class ListPostsCommand : ICommand
    {
        private readonly IPostService postService;

        public ListPostsCommand(IPostService postService)
        {
            this . postService = postService;
        }
        public string Execute(params string[] args)
        {
            var posts = postService.All()
                .GroupBy(p=>p.Category)
                .ToArray();

            var sb = new StringBuilder();
            foreach (var group in posts)
            {
                sb.AppendLine($"Category: {group.Key.Name}");
                foreach (var post in group)
                {
                    sb.AppendLine($"-{post.Id}. {post.Title}. {post.Content} by {post.Author.Username}");                  
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
