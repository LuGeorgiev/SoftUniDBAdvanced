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

            //builder.HasData(
            //    new Town {Id=1,Name="Sofia",Country="Bulgaria" },
            //    new Town {Id=2,Name="Varna",Country="Bulgaria" },
            //    new Town {Id=3,Name="Burgas",Country="Bulgaria" },
            //    new Town {Id=4,Name="Plovdiv",Country="Bulgaria" },
            //    new Town {Id=5,Name="Vraca",Country="Bulgaria" },
            //    new Town {Id=6,Name="Pernik",Country="Bulgaria" },
            //    new Town {Id=7,Name="Bansko",Country="Bulgaria" }
            //    );
        }
    }
}
