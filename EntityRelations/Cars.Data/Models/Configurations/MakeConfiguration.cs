using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Data.Models.Configurations
{
    class MakeConfiguration : IEntityTypeConfiguration<Make>
    {

        public void Configure(EntityTypeBuilder<Make> builder)
        {
            
            //Many to one
            builder
                    .HasMany(m => m.Cars)
                    .WithOne(c => c.Make)
                    .HasForeignKey(c => c.MakeId);
        }
    }
}
