using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Data.Models.Configurations
{
    class EngineConfiguration : IEntityTypeConfiguration<Engine>
    {
        public void Configure(EntityTypeBuilder<Engine> builder)
        {
            //One many
            builder
                .HasMany(e => e.Cars)
                .WithOne(c => c.Engine)
                .HasForeignKey(e => e.EngineId);
        }
    }
}
