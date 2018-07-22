using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Instagraph.Data;
using Newtonsoft.Json;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var uncommentedPosts = context.Posts
                .Where(p => p.Comments.Count == 0)
                .OrderBy(p => p.Id)
                .Select(p => new
                {
                    Id=p.Id,
                    Picture = p.Picture.Path,
                    User=p.User.Username
                }).ToList();

            string result = JsonConvert.SerializeObject(uncommentedPosts, Formatting.Indented);

            return result;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var usersWithPosts = context.Users
                .Where(u => u.Posts.Count > 0 && u.Followers.Count > 0)
                .ToList();
            var popularUserId = new List<int>();

            foreach (var user in usersWithPosts)
            {
                var userIdFollowers = user.Followers
                    .Select(x => x.FollowerId)
                    .ToList();
                foreach (var posts in user.Posts)
                {
                    foreach (var comment in posts.Comments)
                    {
                        if (userIdFollowers.Contains(comment.User.Id))
                        {
                            popularUserId.Add(user.Id);
                        }
                    }
                }
            }

            //Bojidar Danchev Approach
            //var users = context.Users
            //    .Where(u => u.Posts
            //        .Any(p => p.Comments
            //            .Any(c => u.Followers
            //                .Any(f => f.FollowerId == c.UserId))))
            //     .OrderBy(u => u.Id)
            //     .ToArray();


            var popularUsers = context.Users
                .Where(u => popularUserId.Contains(u.Id))
                .OrderBy(u => u.Id)
                .Select(u => new
                {
                    Username = u.Username,
                    Followers = u.Followers.Count
                })
                .ToList();


            string result = JsonConvert.SerializeObject(popularUsers, Formatting.Indented);

            return result;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var userMostPopularPost = context.Users
                .Select(u => new
                {
                    Username = u.Username,
                    MostComments = u.Posts.Count > 0
                            ? u.Posts.Max(p => p.Comments.Count)
                            : 0
                })
                .OrderByDescending(x => x.MostComments)
                .ThenBy(x => x.Username)
                .ToList();

            var xDoc = new XDocument();

            xDoc.Add(new XElement("users")); 
            foreach (var n in userMostPopularPost)
            {
                xDoc.Root.Add(new XElement("user", 
                    new XElement("Username", n.Username), 
                    new XElement("MostComments",n.MostComments)));
            }

            string xmlString = xDoc.ToString();
            return xmlString;
        }
    }
}
