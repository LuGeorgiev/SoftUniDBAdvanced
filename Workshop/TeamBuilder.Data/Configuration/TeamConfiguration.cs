using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.HasMany(x => x.Invittaions)
                .WithOne(x => x.Team)
                .HasForeignKey(x => x.TeamId);

        }
    }
}
