using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.Models;
using Instagraph.DataProcessor.ModelsDto;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        private static string ErrorMsg = "Error: Invalid data.";
        private static string SuccessMsgPicture = "Successfully imported Picture {0}.";
        private static string SuccessMsgUser = "Successfully imported User {0}.";
        private static string SuccessMsgPost = "Successfully imported Post {0}.";
        private static string SuccessMsgComment = "Successfully imported Comment {0}.";
        private static string SuccessMsgUserFollower = "Successfully imported Follower {0} to User {1}.";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();
            Picture[] pictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);
            var picturesToAdd = new List<Picture>();

            foreach (var picture in pictures)
            {
                bool validPath = context.Pictures
                    .Any(p => p.Path == picture.Path) ||
                    picturesToAdd.Any(p => p.Path == picture.Path);

                if (!string.IsNullOrWhiteSpace(picture.Path)&&picture.Size>0&&!validPath)
                {
                    picturesToAdd.Add(picture);                    
                    sb.AppendLine(string.Format(SuccessMsgPicture,picture.Path));
                }
                else
                {
                    sb.Append(ErrorMsg);
                }
            }
            context.Pictures.AddRange(picturesToAdd);
            context.SaveChanges();
            
            return sb.ToString().TrimEnd();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var deserializedUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);
            var users = new List<User>();

            foreach (var userDto in deserializedUsers)
            {         
                bool isValid = !String.IsNullOrWhiteSpace(userDto.Username)
                    && userDto.Username.Length<=30
                    && !String.IsNullOrWhiteSpace(userDto.Password)
                    && userDto.Password.Length<=20
                    && !String.IsNullOrWhiteSpace(userDto.ProfilePicture);

                var picture = context.Pictures
                    .FirstOrDefault(p => p.Path == userDto.ProfilePicture);
                bool userExists = users
                    .Any(u => u.Username == userDto.Username);

                if (picture==null||!isValid||userExists)
                {
                    sb.Append(ErrorMsg);
                    continue;
                }
                var user = Mapper.Map<User>(userDto);
                user.ProfilePicture = picture;
                users.Add(user);
               
                sb.AppendLine(string.Format(SuccessMsgUser,user.Username));                
            }
            context.Users.AddRange(users);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();
            UserFollowerDto[] deserialiizedUserFollower = JsonConvert.DeserializeObject<UserFollowerDto[]>(jsonString);
            var userFollowers = new List<UserFollower>();

            foreach (var userFollowerDto in deserialiizedUserFollower)
            {
                var user = context.Users
                    .FirstOrDefault(uf => uf.Username == userFollowerDto.User);
                var follower = context.Users
                    .FirstOrDefault(uf => uf.Username == userFollowerDto.Follower);

                if (user==null || follower==null || user==follower)
                {
                    sb.AppendLine(ErrorMsg);
                    continue;
                }   
                
                var userFollower = new UserFollower
                {
                    User=user,
                    UserId=user.Id,
                    Follower=follower,
                    FollowerId=follower.Id
                };

                if (userFollowers.Any(p=>p.FollowerId==userFollower.FollowerId&&p.UserId==userFollower.UserId))
                {
                    continue;
                }
                userFollowers.Add(userFollower);
                sb.AppendFormat(string.Format(SuccessMsgUserFollower, userFollowerDto.Follower, userFollowerDto.User));
            }
            context.UserFollowers.AddRange(userFollowers);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var xmlDoc = XDocument.Parse(xmlString);
            var elements = xmlDoc.Root.Elements();
            var posts = new List<Post>();
            var sb = new StringBuilder();

            foreach (var element in elements)
            {
                string caption = element.Element("caption")?.Value;
                string username = element.Element("user")?.Value;
                string picturePath = element.Element("picture")?.Value;

                var user = context.Users
                    .FirstOrDefault(u => u.Username == username);
                var picture = context.Pictures
                    .FirstOrDefault(p => p.Path == picturePath);

                if (user==null||picture==null)
                {
                    sb.AppendLine(ErrorMsg);
                    continue;
                }

                posts.Add(new Post
                {
                    Caption=caption,
                    User=user,
                    Picture=picture                    
                });
                sb.AppendLine(string.Format(SuccessMsgPost, caption));
            }
            context.Posts.AddRange(posts);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var xmlDoc = XDocument.Parse(xmlString);
            var elements = xmlDoc.Root.Elements();
            var comments = new List<Comment>();
            var sb = new StringBuilder();

            foreach (var element in elements)
            {
                string content = element.Element("content")?.Value;
                string username = element.Element("user")?.Value;
                string postIdString = element.Element("post")?.Attribute("id")?.Value;

                bool hasNullImput = String.IsNullOrWhiteSpace(content)
                    ||String.IsNullOrWhiteSpace(username)
                    ||String.IsNullOrWhiteSpace(postIdString);
                if (hasNullImput)
                {
                    sb.AppendLine(ErrorMsg);
                    continue;
                }
                int postId = int.Parse(postIdString);

                var post = context.Posts
                    .FirstOrDefault(p => p.Id == postId);
                var user = context.Users
                    .FirstOrDefault(u => u.Username == username);
                if (post == null || user == null)
                {
                    sb.AppendLine(ErrorMsg);
                    continue;
                }

                comments.Add(new Comment
                {
                    Content = content,
                    User = user,
                    PostId = postId
                });
                sb.AppendLine(string.Format(SuccessMsgComment, content));
            }
            context.Comments.AddRange(comments);
            context.SaveChanges();

            return sb.ToString().Trim();
        }
    }
}
