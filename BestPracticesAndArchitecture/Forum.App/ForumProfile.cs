
using AutoMapper;
using Forum.App.Models;
using Forum.Models;

namespace Forum.App
{
    public class ForumProfile:Profile
    {
        //Initialize Automapper
        public ForumProfile()
        {
            CreateMap<User, User>();

            CreateMap<Post, PostDetailsDto>()
                .ForMember(
                    dto => dto.ReplyCount,
                    dest => dest.MapFrom(post => post.Replies.Count));
            CreateMap<Reply, ReplyDto>(); 
        }
    }
}
