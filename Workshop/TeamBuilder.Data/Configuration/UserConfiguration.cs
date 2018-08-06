using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TeamBuilder.Models;
using TeamBuilder.Models.Enumarations;

namespace TeamBuilder.Data.Configuration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => x.Username)
                .IsUnique();

            builder.Property(x => x.Gender)
                .HasConversion(
                    v=>v.ToString(),
                    v=>(Gender)Enum.Parse(typeof(Gender), v,true))
                    .HasMaxLength(10);

            builder.HasMany(x => x.CreatedEvents)
                .WithOne(x => x.Creator)
                .HasForeignKey(x => x.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.CreatedTeams)
                .WithOne(x => x.Creator)
                .HasForeignKey(x => x.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);                       

            builder.HasMany(u => u.ReceivedInvitations)
                .WithOne(i => i.InvitedUser)
                .HasForeignKey(i => i.InvitedUserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
