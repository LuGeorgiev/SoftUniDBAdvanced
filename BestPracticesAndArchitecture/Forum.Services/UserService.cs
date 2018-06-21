using Forum.Models;
using Forum.Data;
using Forum.Services.Contracts;
using System;
using System.Linq;

namespace Forum.Services
{
    public class UserService : IUserService
    {
        private readonly ForumDbContext context;

        public UserService(ForumDbContext context)
        {
            this.context = context;
        }

        public User Create(string username, string password)
        {
            var user = new User(username, password);
            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }

        public void Delete(int id)
        {
            var user = context.Users.Find(id);
            context.Remove(user);
            context.SaveChanges();
        }

        public User FindByUsername(string username)
        {
            var user = context.Users
                .SingleOrDefault(u => u.Username == username);

            return user;
        }

        public User FindByUsernameAndPAssword(string username, string password)
        {
            var user = context.Users
                .SingleOrDefault(u => u.Username == username&&u.Password==password);

            return user;
        }

        public User FindUserByID(int Id)
        {
            var user = context.Users.Find(Id);

            return user;
        }
    }
}
