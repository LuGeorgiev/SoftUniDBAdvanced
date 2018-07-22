using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Instagraph.Data.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(e => e.Username)
               .IsUnique(); //or define as AlternateKey

            builder.HasOne(x => x.ProfilePicture)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.ProfilePictureId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Followers)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.UsersFollowing)
                .WithOne(x => x.Follower)
                .HasForeignKey(x => x.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
