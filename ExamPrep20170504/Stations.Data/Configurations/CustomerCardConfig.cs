using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;
using Stations.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stations.Data.Configurations
{
    public class CustomerCardConfig : IEntityTypeConfiguration<CustomerCard>
    {
        public void Configure(EntityTypeBuilder<CustomerCard> builder)
        {
            builder.Property(x => x.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (CardType)Enum.Parse(typeof(CardType), v, true))
                .HasDefaultValue(CardType.Normal)
                .HasMaxLength(11);
        }
    }
}
