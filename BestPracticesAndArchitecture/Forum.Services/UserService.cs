using Forum.Models;
using Forum.Data;
using Forum.Services.Contracts;
using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Forum.Services
{
    public class UserService : IUserService
    {
        private readonly ForumDbContext context;

        public UserService(ForumDbContext context)
        {
            this.context = context;
        }

        public TModel Create<TModel>(string username, string password)
        {
            var user = new User(username, password);
            context.Users.Add(user);
            context.SaveChanges();

            var userDto = Mapper.Map<TModel>(user);

            return userDto;
        }

        public void Delete(int id)
        {
            var user = context.Users.Find(id);
            context.Remove(user);
            context.SaveChanges();
        }

        public TModel FindByUsername<TModel>(string username)
        {
            //var user = context.Users
            //    .SingleOrDefault(u => u.Username == username);

            var user = context.Users
                .Where(u => u.Username == username)
                .ProjectTo<TModel>()
                .SingleOrDefault();

            return user;
        }

        public TModel FindByUsernameAndPassword<TModel>(string username, string password)
        {
            //    var user = context.Users
            //        .SingleOrDefault(u => u.Username == username&&u.Password==password);

            var user = context.Users
                .Where(u => u.Username == username && u.Password == password)
                .ProjectTo<TModel>()
                .SingleOrDefault();

            return user;
        }

        public TModel FindUserByID<TModel>(int Id)
        {
            //var user = context.Users.Find(Id);

            var user = context.Users
                .Where(u => u.Id == Id)
                .ProjectTo<TModel>()
                .SingleOrDefault();

            return user;
        }
    }
}
