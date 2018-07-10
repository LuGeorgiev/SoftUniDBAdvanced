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
        }
    }
}
