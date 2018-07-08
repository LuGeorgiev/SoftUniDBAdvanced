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
            var username = data[2];
            var permission = data[3];

            using (var db = new PhotoShareContext())
            {
                var album = db.Albums
                    .Find(albumId);
                if (album==null)
                {
                    throw new ArgumentException($"Album {albumId} not found!");
                }

                var user = db.Users
                    .FirstOrDefault(u => u.Username == username);
                if (user==null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                var isRolevalid = Enum.TryParse(typeof(Role), permission, true, out object role);
                if (!isRolevalid)
                {
                    throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
                }

                album.AlbumRoles.Add(new AlbumRole()
                {
                    User=user,
                    Role=(Role)role
                });
                db.SaveChanges();

                return $"Username {username} added to album {album.Name} ({role.ToString()})";
            }
        }
    }
}
