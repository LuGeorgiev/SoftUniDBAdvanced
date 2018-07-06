namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            var requesterUsername = data[1];
            var addedFriendUsername = data[2];

            using (var context = new PhotoShareContext())
            {
                var requestingUser = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username == requesterUsername);
                if (requestingUser==null)
                {
                    throw new ArgumentException($"{requesterUsername} was not found");
                }

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
                    throw new InvalidOperationException($"{addedFriendUsername} is already frind with {requesterUsername}");
                }
                else if (!alreadyFriends && accepted)
                {
                    throw new InvalidOperationException($"{requesterUsername} has already friend.. .. {addedFriendUsername}");
                }

                requestingUser.FriendsAdded.Add(
                    new Friendship()
                    {
                        User=requestingUser,
                        Friend=addedFriend
                    });

                context.SaveChanges();
                return $"Frien {addedFriendUsername} added to {requesterUsername}";
            }
        }
    }
}
