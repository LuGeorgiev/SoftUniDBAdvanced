namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class UploadPictureCommand : ICommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(string[] data)
        {
            var albumName = data[1];
            var pictureTitle = data[2];
            var pictureFilePath = data[3];

            using (var db = new PhotoShareContext())
            {
                var album = db.Albums
                    .FirstOrDefault(x => x.Name == albumName);
                if (album==null)
                {
                    throw new ArgumentException($"Album {albumName} not found!");
                }

                album.Pictures.Add(new Picture()
                {
                    Title=pictureTitle,
                    Path=pictureFilePath,
                    
                });
                db.SaveChanges();
            }
            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}
