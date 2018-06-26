using Forum.App.Commands.Contracts;
using Forum.Services.Contracts;
using AutoMapper.QueryableExtensions;
using System.Text;
using System.Linq;
using Forum.App.Models;

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
            //Automapper approach
            var posts = postService
                .All<PostDto>()                
                .GroupBy(p => p.CategoryName)
                .ToArray();

            //var posts = postService.All()
            //    .GroupBy(p=>p.Category)
            //    .ToArray();

            var sb = new StringBuilder();
            foreach (var group in posts)
            {
                sb.AppendLine($"Category: {group.Key}");
                foreach (var post in group)
                {
                    sb.AppendLine($"-{post.Id}. {post.Title}. {post.Content} by {post.AuthorUsername}");                  
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
