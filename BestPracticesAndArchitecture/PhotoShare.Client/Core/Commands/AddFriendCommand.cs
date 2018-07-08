namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddFriendCommand : ICommand
    {
        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            //Refactored in P02
            //var requesterUsername = data[1];
            var addedFriendUsername = data[1];

            using (var context = new PhotoShareContext())
            {
                //var requestingUser = context.Users
                //    .Include(u => u.FriendsAdded)
                //    .ThenInclude(fa => fa.Friend)
                //    .FirstOrDefault(u => u.Username == requesterUsername);
                //if (requestingUser==null)
                //{
                //    throw new ArgumentException($"{requesterUsername} was not found");
                //}

                //P02.Extend Photo Share System refactoring
                if (Session.User == null)
                {
                    throw new InvalidOperationException("Invalid credentials! Please, Login.");
                }
                var requestingUser = Session.User;

                var addedFriend = context.Users
                    .Include(u=>u.FriendsAdded)
                    .ThenInclude(fa=>fa.Friend)
                    .FirstOrDefault(u => u.Username == addedFriendUsername);
                if (addedFriend == null)
                {
                    throw new ArgumentException($"{addedFriendUsername} was not found");
                }

                bool alreadyFriends = requestingUser.FriendsAdded.Any(u => u.Friend == addedFriend);
                bool accepted = addedFriend.FriendsAdded.Any(u => u.Friend == requestingUser);
                if (alreadyFriends && !accepted)
                {
                    throw new InvalidOperationException("Friend request was alreday sent!");
                }
                else if (alreadyFriends && accepted)
                {
                    throw new InvalidOperationException($"{addedFriendUsername} is already frind with {requestingUser.Username}");
                }
                else if (!alreadyFriends && accepted)
                {
                    throw new InvalidOperationException($"{requestingUser.Username} has already friend.. .. {addedFriendUsername}");
                }

                requestingUser.FriendsAdded.Add(
                    new Friendship()
                    {
                        User=requestingUser,
                        Friend=addedFriend
                    });

                context.SaveChanges();
                return $"Friend {addedFriendUsername} added to {requestingUser.Username}";
            }
        }
    }
}
