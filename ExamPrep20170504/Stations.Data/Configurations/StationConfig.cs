using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;
using System;


namespace Stations.Data.Configurations
{
    public class StationConfig : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.HasAlternateKey(x => x.Name);
                
        }
    }
}
