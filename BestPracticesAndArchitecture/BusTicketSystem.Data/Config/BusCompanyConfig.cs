using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketsSystem.Data.Config
{
    internal class BusCompanyConfig : IEntityTypeConfiguration<BusCompany>
    {
        public void Configure(EntityTypeBuilder<BusCompany> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(20);

            //builder.HasData(
            //    new BusCompany {Id=11,Name="Fine Travel" }, 
            //    new BusCompany { Id = 2, Name = "Do not look back" },
            //    new BusCompany { Id = 3, Name = "Fly away" },
            //    new BusCompany { Id = 4, Name = "Fast and Furious" },
            //    new BusCompany { Id = 5, Name = "Last but save" }
            //    );            
        }
    }
}
