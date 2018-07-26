using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;
using Stations.Models.Enumerations;
using System;

namespace Stations.Data.Configurations
{
    public class SeatingClassConfig : IEntityTypeConfiguration<SeatingClass>
    {
        public void Configure(EntityTypeBuilder<SeatingClass> builder)
        {
            builder.HasAlternateKey(x => x.Name);

            builder.HasAlternateKey(x => x.Abbreviation);
        }
    }
}
