﻿namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Contracts;
    using System;

    public class PrintFriendsListCommand : ICommand
    {
        // PrintFriendsList <username>
        public string Execute(string[] data)
        {
            // TODO prints all friends of user with given username.
            throw new NotImplementedException();
        }
    }
}
