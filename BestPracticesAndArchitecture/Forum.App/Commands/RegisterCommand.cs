using Forum.App.Commands.Contracts;
using Forum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.App.Commands
{
    public class RegisterCommand : ICommand
    {
        private readonly IUserService userService;
        public RegisterCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] args)
        {
            var userName = args[0];
            var password = args[1];

            var existingUser = userService.FindByUsername(userName);
            if (existingUser!=null)
            {
                return "There is existing user with that userName!";
            }

            userService.Create(userName, password);

            return "User created successfully!";
        }
    }
}
