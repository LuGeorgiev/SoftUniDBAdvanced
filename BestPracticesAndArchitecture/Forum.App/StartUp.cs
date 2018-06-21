using Forum.Data;
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
            var engine = new Engine(serviceProvider);
            engine.Run();
            

            //var userService = serviceProvider.GetService<IUserService>();
            //var userById = userService.FindUserByID(4); //need instance of context in UserSErvice
            
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

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
