namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CreateAlbumCommand:ICommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        
        public string Execute(string[] data)
        {
            var username = data[1];
            var albumTitle = data[2];
            var color = data[3];
            var tags = data.Skip(4);

            using (var db=new PhotoShareContext())
            {
                var user = db.Users
                    .FirstOrDefault(u => u.Username == username);

                if (user==null)
                {
                    throw new ArgumentException($"User {username} do not exists!");
                }

                var albums = db.Users
                    .Single(u => u.Username == username)
                    .AlbumRoles
                    .Select(a=>new { a.Album.Name});
                if (albums.Any(a=>a.Name==albumTitle))
                {
                    throw new ArithmeticException($"User- {username} already has album - {albumTitle}");
                }

                var isColorValid = Enum.TryParse(color, true, out Color backgroundColor);
                if (!isColorValid)
                {
                    throw new ArgumentException($"Color {color} was not found");
                }

                
                foreach (var tag in tags)
                {
                    var tagToAdd = db.Tags
                        .FirstOrDefault(t => t.Name == tag);
                    if (tagToAdd==null)
                    {
                        throw new ArgumentException($"Invalid tags!");
                    }                    
                }

                var album = new Album
                {
                    Name=albumTitle,
                    BackgroundColor=backgroundColor,
                    
                };
                var albumRole = new AlbumRole
                {                    
                    Role = Role.Owner,
                    User = user,
                    Album=album
                };
                user.AlbumRoles.Add(albumRole);
                //album.AlbumRoles.Add(albumRole);

                foreach (var tag in tags)
                {
                    var currentTag = db.Tags.First(t => t.Name == tag);
                    var albumTag = new AlbumTag
                    {
                        Tag=currentTag,
                        Album=album
                    };

                    album.AlbumTags.Add(albumTag);
                }
                db.SaveChanges();                    
            }
            return $"Album {albumTitle} successfully created!";
        }
    }
}
