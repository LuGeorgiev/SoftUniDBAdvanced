using Forum.App.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.App.Commands
{
    public class LogoutCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            if (Session.User==null)
            {
                return "You are not logged in";
            }
            Session.User = null;

            return "Successfully logged out!";
        }
    }
}
