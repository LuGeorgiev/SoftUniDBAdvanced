using Forum.App.Commands.Contracts;
using Forum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.App.Commands
{
    public class ReplyCommand : ICommand
    {
        private readonly IReplyService replyService;

        public ReplyCommand(IReplyService replyService)
        {
            this.replyService = replyService;
        }

        public string Execute(params string[] args)
        {
            var postId = int.Parse(args[0]);
            var content = args[1];
            if (Session.User==null)
            {
                return "You are not logged in!";
            }
            var authorId = Session.User.Id;

            replyService.Create(content, postId, authorId);

            return $"Reply created successfully!";
        }
    }
}
