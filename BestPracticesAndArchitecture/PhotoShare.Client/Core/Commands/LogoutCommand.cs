using PhotoShare.Client.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Client.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        public string Execute(string[] data)
        {
            if (Session.User==null)
            {
                throw new InvalidOperationException("You should log in first in order to logout.");
            }
            var currentUsername = Session.User.Username;

            Session.User = null;
            return $"User {currentUsername} successfully logged out!";
        }
    }
}
