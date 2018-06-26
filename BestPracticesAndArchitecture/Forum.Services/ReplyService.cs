using AutoMapper;
using Forum.Data;
using Forum.Models;
using Forum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Services
{
    public class ReplyService : IReplyService
    {
        private readonly ForumDbContext context;

        public ReplyService(ForumDbContext context)
        {
            this.context = context;
        }

        public TModel Create<TModel>(string replyText, int postId, int authorId)
        {
            var reply = new Reply()
            {
                Content=replyText,
                PostId=postId,
                AuthorId=authorId
            };

            context.Replies.Add(reply);
            context.SaveChanges();
            //return reply;

            var replyDto = Mapper.Map<TModel>(reply);

            return replyDto;
        }

        public void Delete(int replyId)
        {
            throw new NotImplementedException();
        }
    }
}
