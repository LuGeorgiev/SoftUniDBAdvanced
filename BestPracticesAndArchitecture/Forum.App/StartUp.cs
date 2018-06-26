using AutoMapper;
using Forum.App.Models;
using Forum.Data;
using Forum.Models;
using Forum.Services;
using Forum.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Forum.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureService();

            //mapping onjects
            InitializeAutomapper();

            var engine = new Engine(serviceProvider);
            engine.Run();
            

            //var userService = serviceProvider.GetService<IUserService>();
            //var userById = userService.FindUserByID(4); //need instance of context in UserSErvice
            
        }

        //Automapper configuration
        private static void InitializeAutomapper()
        {
            //with ForumProfule
            //This way if it is not Initalized in IserviceProvider
            //Mapper.Initialize(cfg => cfg.AddProfile<ForumProfile>());

                //Initial way
            //Mapper.Initialize(cfg=> 
            //{
            //    cfg.CreateMap<Post, PostDetailsDto>()
            //    .ForMember(
            //        dto=>dto.ReplyCount,
            //        dest=>dest.MapFrom(post=>post.Replies.Count));
            //    cfg.CreateMap<Reply, ReplyDto>();
            //});
        }

        private static IServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<ForumDbContext>(options => 
                options.UseSqlServer(Configuration.ConnectionString)
            ); //instance fo dB context

            serviceCollection.AddTransient<IDbInitializerService, DatabaseInitializerService>();
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<ICategoryService, CategoryService>();
            serviceCollection.AddTransient<IPostService, PostService>();
            serviceCollection.AddTransient<IReplyService, ReplyService>();

            //Initialize Automapper when DI is needed
            //From NUget - install-package Automapper.Extensions.Microsoft.DependencyInjection
            serviceCollection.AddAutoMapper(cfg=>cfg.AddProfile<ForumProfile>());

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
