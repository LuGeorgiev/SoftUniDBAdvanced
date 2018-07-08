namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddTagToCommand :ICommand
    {
        // AddTagTo <albumName> <tag>
        public string Execute(string[] data)
        {
            var albumTitle = data[1];
            var tag = data[2];

            using (var db = new PhotoShareContext())
            {
                var albumToAddTag = db.Albums
                    .FirstOrDefault(a => a.Name == albumTitle);
                var tagToAdd = db.Tags
                    .FirstOrDefault(t => t.Name == tag);

                if (albumToAddTag==null||tagToAdd==null)
                {
                    throw new ArgumentException($"Either tag or album do not exist!");
                }

                var albumTag = new AlbumTag
                {
                    Album = albumToAddTag,
                    Tag = tagToAdd
                };

                db.AlbumTags.Add(albumTag);
                db.SaveChanges();                
            }
            return $"Tag {tag} added to {albumTitle}!";
        }
    }
}
