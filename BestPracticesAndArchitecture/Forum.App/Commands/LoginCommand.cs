using Forum.App.Commands.Contracts;
using Forum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.App.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] args)
        {
            var username = args[0];
            var password = args[1];

            //TODO - need refactoring. cannot work like that
            //var user = userService.FindByUsernameAndPassword(username, password);

            //if (user==null)
            //{
            //    return "Invalid username or password";
            //}
            //Session.User = user;

            return $"Logged in successfully";
        }
    }
}
