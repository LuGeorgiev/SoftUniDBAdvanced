using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;
using Stations.Models.Enumerations;
using System;

namespace Stations.Data.Configurations
{
    public class TrainConfig : IEntityTypeConfiguration<Train>
    {
        public void Configure(EntityTypeBuilder<Train> builder)
        {
            builder.HasAlternateKey(t => t.TrainNumber);
                

            builder.Property(x => x.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (TrainType)Enum.Parse(typeof(TrainType), v, true))
                .HasMaxLength(12)
                .IsRequired(false);
                
        }
    }
}
