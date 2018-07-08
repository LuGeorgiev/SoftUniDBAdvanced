namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AcceptFriendCommand:ICommand
    {
        // AcceptFriend <username1> <username2>
        public string Execute(string[] data)
        {
            var firstUsernam = data[1];
            var secondUsernam = data[1];

            using (var db = new PhotoShareContext())
            {
                var firstUser = db.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(f => f.Friend)
                    .Include(u => u.AddedAsFriendBy)
                    .ThenInclude(f => f.Friend)
                    .FirstOrDefault(u => u.Username == firstUsernam);
                if (firstUser==null)
                {
                    throw new ArgumentException($"{firstUsernam} not found!");
                }

                var secondUser = db.Users
                   .Include(u => u.FriendsAdded)
                   .ThenInclude(f => f.Friend)
                   .Include(u => u.AddedAsFriendBy)
                   .ThenInclude(f => f.Friend)
                   .FirstOrDefault(u => u.Username == secondUsernam);
                if (secondUser == null)
                {
                    throw new ArgumentException($"{secondUsernam} not found!");
                }

                if (firstUser.FriendsAdded.Any(f=>f.Friend.Username==secondUsernam))
                {
                    throw new InvalidOperationException($"{secondUsernam} is already a friend to {firstUsernam}");
                }
                if (firstUser.AddedAsFriendBy.Any(f=>f.Friend.Username==secondUsernam))
                {
                    throw new InvalidOperationException($"{secondUsernam} has not added {firstUsernam} as a friend");
                }

                firstUser.FriendsAdded.Add(new Friendship()
                {
                    User=firstUser,
                    Friend=secondUser
                });
                db.SaveChanges();

                return $"{firstUsernam} accepted {secondUsernam} as a friend";
            }
        }
    }
}
