namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class ShareAlbumCommand : ICommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(string[] data)
        {
            var albumId = int.Parse(data[1]);
            //var username = data[2]; //P02 user in no longer needed from input
            var permission = data[2];

            using (var db = new PhotoShareContext())
            {
                var album = db.Albums
                    .Find(albumId);
                if (album==null)
                {
                    throw new ArgumentException($"Album {albumId} not found!");
                }

                //P02 - refactor
                //var user = db.Users
                //    .FirstOrDefault(u => u.Username == username);
                //if (user==null)
                //{
                //    throw new ArgumentException($"User {username} not found!");
                //}
                //P02.Extend Photo Share System refactoring
                if (Session.User == null)
                {
                    throw new InvalidOperationException("Invalid credentials! ");
                }
                var user = Session.User;

                var isRoleValid = Enum.TryParse(typeof(Role), permission, true, out object role);
                if (!isRoleValid)
                {
                    throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
                }

                album.AlbumRoles.Add(new AlbumRole()
                {
                    User=user,
                    Role=(Role)role
                });
                db.SaveChanges();

                return $"Username {user.Username} added to album {album.Name} ({role.ToString()})";
            }
        }
    }
}
