using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(x => new { x.UserId, x.TeamId });

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserTeams)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Team)
                .WithMany(x => x.UserTeams)
                .HasForeignKey(x => x.TeamId);
        }
    }
}
