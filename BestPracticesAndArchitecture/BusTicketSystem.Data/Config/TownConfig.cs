using System;
using System.Collections.Generic;
using System.Text;
using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketsSystem.Data.Config
{
    class TownConfig : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.Property(x => x.Name)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(x => x.Country)
                .IsUnicode(false)
                .HasMaxLength(20);
        }
    }
}
