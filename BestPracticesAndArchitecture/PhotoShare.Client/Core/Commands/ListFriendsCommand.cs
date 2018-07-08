using Microsoft.EntityFrameworkCore;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Data;
using System;
using System.Linq;
using System.Text;

namespace PhotoShare.Client.Core.Commands
{
    public class ListFriendsCommand : ICommand
    {
        //ListFriends <username>
        public string Execute(string[] data)
        {
            var username = data[1];
            var sb = new StringBuilder();

            using (var db = new PhotoShareContext())
            {
                var usersFriendsToList = db.Users
                    .Include(u=>u.FriendsAdded)                    
                    .FirstOrDefault(u => u.Username == username);
                    
                if (usersFriendsToList==null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if (usersFriendsToList.FriendsAdded.Count==0)
                {
                    sb.AppendLine("No friends for this user. :(");
                }
                else
                {
                    sb.AppendLine("Friends:");
                    foreach (var friend in usersFriendsToList.FriendsAdded)
                    {
                        sb.AppendLine($"-{friend.Friend.Username}");
                    }
                }
            }
            return sb.ToString().TrimEnd();
        }
    }
}
