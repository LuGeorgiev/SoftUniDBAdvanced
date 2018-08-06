using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            throw new NotImplementedException();
        }
    }
}
