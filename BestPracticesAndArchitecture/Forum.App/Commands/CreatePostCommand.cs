using Forum.App.Commands.Contracts;
using Forum.Models;
using Forum.Services.Contracts;
using System;


namespace Forum.App.Commands
{
    public class CreatePostCommand : ICommand
    {
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;

        public CreatePostCommand(IPostService postService, ICategoryService categoryService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
        }


        public string Execute(params string[] args)
        {
            var categoryName = args[0];
            var postTitle = args[1];
            var postContent = args[2];

            if (Session.User==null)
            {
                return "You are not logged in!";
            }

            var category = categoryService.FindByName(categoryName);
            if (category==null)
            {
                category = categoryService.Create(categoryName);
            }

            var authorId = Session.User.Id;
            var categoryId = category.Id;

            var post = postService.Create( postTitle, postContent, category.Id, authorId);

            return $"Post with id{post.Id} created successfully!";
        }
    }
}
